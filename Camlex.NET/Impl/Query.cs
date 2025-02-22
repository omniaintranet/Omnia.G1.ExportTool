﻿#region Copyright(c) Alexey Sadomov, Vladimir Timashkov. All Rights Reserved.
// -----------------------------------------------------------------------------
// Copyright(c) 2010 Alexey Sadomov, Vladimir Timashkov. All Rights Reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
//   1. No Trademark License - Microsoft Public License (Ms-PL) does not grant you rights to use
//      authors names, logos, or trademarks.
//   2. If you distribute any portion of the software, you must retain all copyright,
//      patent, trademark, and attribution notices that are present in the software.
//   3. If you distribute any portion of the software in source code form, you may do
//      so only under this license by including a complete copy of Microsoft Public License (Ms-PL)
//      with your distribution. If you distribute any portion of the software in compiled
//      or object code form, you may only do so under a license that complies with
//      Microsoft Public License (Ms-PL).
//   4. The names of the authors may not be used to endorse or promote products
//      derived from this software without specific prior written permission.
//
// The software is licensed "as-is." You bear the risk of using it. The authors
// give no express warranties, guarantees or conditions. You may have additional consumer
// rights under your local laws which this license cannot change. To the extent permitted
// under your local laws, the authors exclude the implied warranties of merchantability,
// fitness for a particular purpose and non-infringement.
// -----------------------------------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Linq;
using CamlexNET.Impl.Helpers;
using CamlexNET.Interfaces;
using CamlexNET.Interfaces.ReverseEngeneering;
using Microsoft.SharePoint.Client;

namespace CamlexNET.Impl
{
    internal class Query : IQuery
    {
        private readonly ITranslatorFactory translatorFactory;
        private readonly IReTranslatorFactory reTranslatorFactory;
        private XElement where;
        private XElement orderBy;
        private XElement groupBy;
        private XElement rowLimit;
        private XElement viewFields;
        private ViewScope viewScope;
        private List<XElement> joins;
        private List<XElement> projectedFields;

        public Query(ITranslatorFactory translatorFactory, IReTranslatorFactory reTranslatorFactory)
        {
            this.translatorFactory = translatorFactory;
            this.reTranslatorFactory = reTranslatorFactory;
        }

        public IQuery Where(Expression<Func<ListItem, bool>> expr)
        {
            expr = this.canonize(expr);
            var translator = translatorFactory.Create(expr);
            this.where = translator.TranslateWhere(expr);
            return this;
        }

        private Expression<Func<ListItem, bool>> canonize(Expression<Func<ListItem, bool>> expr)
        {
            if (expr == null)
            {
                throw new NullReferenceException("Expression is null");
            }

            if ((expr.Body.NodeType == ExpressionType.AndAlso || expr.Body.NodeType == ExpressionType.OrElse))
            {
                var left = this.canonizeBody(((BinaryExpression) expr.Body).Left);
                var right = this.canonizeBody(((BinaryExpression)expr.Body).Right);
                return Expression.Lambda<Func<ListItem, bool>>(expr.Body.NodeType == ExpressionType.AndAlso ? Expression.AndAlso(left, right) : Expression.OrElse(left, right),
                    Expression.Parameter(typeof(ListItem), ReflectionHelper.CommonParameterName));
            }

            // canonize boolean expression with explicit cast to equal expression in order to reuse existing code
            var exprBody = this.canonizeBody(expr.Body);
            return Expression.Lambda<Func<ListItem, bool>>(exprBody, Expression.Parameter(typeof(ListItem), ReflectionHelper.CommonParameterName));
        }

        private Expression canonizeBody(Expression expr)
        {
            if ((expr.NodeType == ExpressionType.AndAlso || expr.NodeType == ExpressionType.OrElse))
            {
                var left = this.canonizeBody(((BinaryExpression)expr).Left);
                var right = this.canonizeBody(((BinaryExpression)expr).Right);
                return (expr.NodeType == ExpressionType.AndAlso ? Expression.AndAlso(left, right) : Expression.OrElse(left, right));
            }

            if (expr.Type == typeof(bool) && expr.NodeType == ExpressionType.Convert)
            {
                expr = Expression.Equal(expr, Expression.Constant(true));
            }
            else if (expr.Type == typeof(bool) && expr.NodeType == ExpressionType.Not)
            {
                expr = Expression.Equal(((UnaryExpression)expr).Operand, Expression.Constant(false));
            }
            return expr;
        }

        public IQuery WhereAll(IEnumerable<Expression<Func<ListItem, bool>>> expressions)
        {
            var combinedExpression = ExpressionsHelper.CombineAnd(expressions);
            return this.Where(combinedExpression);
        }

        public IQuery WhereAll(string existingWhere, Expression<Func<ListItem, bool>> expression)
        {
            return this.WhereAll(existingWhere, new List<Expression<Func<ListItem, bool>>>(new[] { expression }));
        }

        public IQuery WhereAll(string existingWhere, IEnumerable<Expression<Func<ListItem, bool>>> expressions)
        {
            var where = this.getWhereExpressionFromString(existingWhere);
            var exprs = new List<Expression<Func<ListItem, bool>>>(expressions);
            exprs.Add(where);
            return this.WhereAll(exprs);
        }

        public IQuery WhereAll(IEnumerable<string> expressions)
        {
            if (expressions == null || !expressions.Any())
            {
                throw new EmptyExpressionsListException();
            }
            var exprs = new List<Expression<Func<ListItem, bool>>>(expressions.Select(getWhereExpressionFromString));
            return this.WhereAll(exprs);
        }

        private Expression<Func<ListItem, bool>> getWhereExpressionFromString(string existingWhere)
        {
            existingWhere = this.ensureParentTag(existingWhere, Tags.Query);
            existingWhere = this.ensureParentTag(existingWhere, Tags.View);

            var translator = this.reTranslatorFactory.Create(existingWhere);
            var expr = translator.TranslateWhere() as Expression<Func<ListItem, bool>>;
            if (expr == null)
            {
                throw new IncorrectCamlException(Tags.Where);
            }
            return expr;
        }

        private string ensureParentTag(string xml, string tag)
        {
            if (!string.IsNullOrEmpty(xml) && !xml.StartsWith(string.Format("<{0}>", tag)))
            {
                // re expects Query tag
                xml = string.Format("<{0}>{1}</{0}>", tag, xml);
            }
            return xml;
        }

        private Expression<Func<ListItem, object[]>> getOrderByExpressionFromString(string existingOrderBy)
        {
            existingOrderBy = this.ensureParentTag(existingOrderBy, Tags.View);

            var translator = this.reTranslatorFactory.Create(existingOrderBy);
            var orderByExpr = translator.TranslateOrderBy();
            var expr = orderByExpr as Expression<Func<ListItem, object[]>>;
            if (expr == null)
            {
                var singleExpr = orderByExpr as Expression<Func<ListItem, object>>;
                if (singleExpr == null)
                {
                    throw new IncorrectCamlException(Tags.OrderBy);
                }
                expr = this.createArrayExpression(singleExpr);
            }
            return expr;
        }

        private Expression<Func<ListItem, object[]>> getGroupByExpressionFromString(string existingGroupBy, out bool? collapse, out int? groupLimit)
        {
            existingGroupBy = this.ensureParentTag(existingGroupBy, Tags.Query);
            existingGroupBy = this.ensureParentTag(existingGroupBy, Tags.View);

            var translator = this.reTranslatorFactory.Create(existingGroupBy);
            GroupByParams groupByParams;
            var orderByExpr = translator.TranslateGroupBy(out groupByParams);
            collapse = groupByParams.HasCollapse ? (bool?) groupByParams.Collapse : null;
            groupLimit = groupByParams.HasGroupLimit ? (int?) groupByParams.GroupLimit : null;

            var expr = orderByExpr as Expression<Func<ListItem, object[]>>;
            if (expr == null)
            {
                var singleExpr = orderByExpr as Expression<Func<ListItem, object>>;
                if (singleExpr == null)
                {
                    throw new IncorrectCamlException(Tags.GroupBy);
                }
                expr = this.ensureArrayExpression(singleExpr);
            }
            return expr;
        }

        private Expression<Func<ListItem, object[]>> getViewFieldsExpressionFromString(string existingViewFields)
        {
            existingViewFields = this.ensureParentTag(existingViewFields, Tags.ViewFields);
            existingViewFields = this.ensureParentTag(existingViewFields, Tags.Query);
            existingViewFields = this.ensureParentTag(existingViewFields, Tags.View);

            var translator = this.reTranslatorFactory.Create(existingViewFields);
            var viewFieldsExpr = translator.TranslateViewFields();

            var expr = viewFieldsExpr as Expression<Func<ListItem, object[]>>;
            if (expr == null)
            {
                var singleExpr = viewFieldsExpr as Expression<Func<ListItem, object>>;
                if (singleExpr == null)
                {
                    throw new IncorrectCamlException(Tags.ViewFields);
                }
                expr = this.ensureArrayExpression(singleExpr);
            }
            return expr;
        }

        public IQuery WhereAny(IEnumerable<Expression<Func<ListItem, bool>>> expressions)
        {
            var combinedExpression = ExpressionsHelper.CombineOr(expressions);
            return this.Where(combinedExpression);
        }

        public IQuery WhereAny(string existingWhere, Expression<Func<ListItem, bool>> expression)
        {
            return this.WhereAny(existingWhere, new List<Expression<Func<ListItem, bool>>>(new[] { expression }));
        }

        public IQuery WhereAny(string existingWhere, IEnumerable<Expression<Func<ListItem, bool>>> expressions)
        {
            var whereExpr = this.getWhereExpressionFromString(existingWhere);
            var exprs = new List<Expression<Func<ListItem, bool>>>(expressions);
            exprs.Add(whereExpr);
            return this.WhereAny(exprs);
        }

        public IQuery WhereAny(IEnumerable<string> expressions)
        {
            if (expressions == null || !expressions.Any())
            {
                throw new EmptyExpressionsListException();
            }
            var exprs = new List<Expression<Func<ListItem, bool>>>(expressions.Select(getWhereExpressionFromString));
            return this.WhereAny(exprs);
        }

        public IQuery OrderBy(Expression<Func<ListItem, object>> expr)
        {
            var lambda = createArrayExpression(expr);
            return OrderBy(lambda);
        }

        public IQuery OrderBy(Expression<Func<ListItem, object[]>> expr)
        {
            var translator = translatorFactory.Create(expr);
            this.orderBy = translator.TranslateOrderBy(expr);
            return this;
        }

        public IQuery OrderBy(IEnumerable<Expression<Func<ListItem, object>>> expressions)
        {
            if (expressions == null || !expressions.Any())
            {
                throw new EmptyExpressionsListException();
            }

            var lambda = createArrayExpression(expressions);
            return OrderBy(lambda);
        }

        public IQuery OrderBy(string existingOrderBy, Expression<Func<ListItem, object>> expr)
        {
            var newExpr = this.createArrayExpression(expr);
            return this.OrderBy(existingOrderBy, newExpr);
        }

        public IQuery OrderBy(string existingOrderBy, Expression<Func<ListItem, object[]>> expr)
        {
            var existingExpr = this.getOrderByExpressionFromString(existingOrderBy);
            var exprs = new List<Expression<Func<ListItem, object[]>>>(new[] { existingExpr, expr });
            var resultExpr = this.createArrayExpression(exprs);
            return OrderBy(resultExpr);
        }

        public IQuery OrderBy(string existingOrderBy, IEnumerable<Expression<Func<ListItem, object>>> expressions)
        {
            var newExpr = this.createArrayExpression(expressions);
            return this.OrderBy(existingOrderBy, newExpr);
        }

        private Expression<Func<ListItem, object[]>> createArrayExpression(IEnumerable<Expression<Func<ListItem, object>>> expressions)
        {
            var expr = expressions.FirstOrDefault();
            if (expr == null)
            {
                throw new EmptyExpressionsListException();
            }

            return Expression.Lambda<Func<ListItem, object[]>>(
                Expression.NewArrayInit(typeof(object), expressions.Select(e => e.Body)), expr.Parameters);
        }

        private Expression<Func<ListItem, object[]>> createArrayExpression(IEnumerable<Expression<Func<ListItem, object[]>>> expressions)
        {
            var expr = expressions.FirstOrDefault();
            if (expr == null)
            {
                throw new EmptyExpressionsListException();
            }

            var list = expressions.SelectMany(e => ((NewArrayExpression) e.Body).Expressions);

            return Expression.Lambda<Func<ListItem, object[]>>(
                Expression.NewArrayInit(typeof(object), list), expr.Parameters);
        }

        private Expression<Func<ListItem, object[]>> ensureArrayExpression(Expression<Func<ListItem, object>> expr)
        {
            Expression<Func<ListItem, object[]>> lambda = null;
            if (expr.Body.Type != typeof(object[]))
            {
                lambda = this.createArrayExpression(expr);
            }
            else
            {
                lambda = Expression.Lambda<Func<ListItem, object[]>>(Expression.NewArrayInit(typeof(object), ((NewArrayExpression) expr.Body).Expressions), expr.Parameters);
            }
            return lambda;
        }

        private Expression<Func<ListItem, object[]>> createArrayExpression(Expression<Func<ListItem, object>> expr)
        {
            return Expression.Lambda<Func<ListItem, object[]>>(Expression.NewArrayInit(typeof(object), expr.Body), expr.Parameters);
        }

        public IQuery GroupBy(Expression<Func<ListItem, object>> expr)
        {
            var lambda = this.createArrayExpression(expr);
            return GroupBy(lambda, null, null);
        }

        public IQuery GroupBy(Expression<Func<ListItem, object[]>> expr, bool? collapse, int? groupLimit)
        {
            var translator = translatorFactory.Create(expr);
            this.groupBy = translator.TranslateGroupBy(expr, collapse, groupLimit);
            return this;
        }

        public IQuery GroupBy(Expression<Func<ListItem, object>> expr, bool? collapse, int? groupLimit)
        {
            var lambda = this.createArrayExpression(expr);
            return GroupBy(lambda, collapse, groupLimit);
        }

        public IQuery GroupBy(Expression<Func<ListItem, object>> expr, bool? collapse)
        {
            var lambda = this.createArrayExpression(expr);
            return GroupBy(lambda, collapse, null);
        }

        public IQuery GroupBy(Expression<Func<ListItem, object>> expr, int? groupLimit)
        {
            var lambda = this.createArrayExpression(expr);
            return GroupBy(lambda, null, groupLimit);
        }

        public IQuery GroupBy(string existingGroupBy, Expression<Func<ListItem, object>> expr)
        {
            var lambda = this.ensureArrayExpression(expr);
            return GroupBy(existingGroupBy, lambda);
        }

        public IQuery GroupBy(string existingGroupBy, Expression<Func<ListItem, object[]>> expr)
        {
            bool? existingCollapse;
            int? existingGroupLimit;
            var existingExpr = this.getGroupByExpressionFromString(existingGroupBy, out existingCollapse, out existingGroupLimit);
            var exprs = new List<Expression<Func<ListItem, object[]>>>(new[] { existingExpr, expr });
            var resultExpr = this.createArrayExpression(exprs);
            return this.GroupBy(resultExpr, existingCollapse, existingGroupLimit);
        }

        public IQuery Take(int count)
        {
            int c = count;
            Expression<Func<int>> expr = () => c;
            var translator = translatorFactory.Create(expr);
            this.rowLimit = translator.TranslateRowLimit(expr);

            return this;
        }

        public IQuery ViewFields(Expression<Func<ListItem, object>> expr)
        {
            var lambda = this.createArrayExpression(expr);
            return ViewFields(lambda);
        }

        public IQuery ViewFields(Expression<Func<ListItem, object[]>> expr)
        {
            var translator = translatorFactory.Create(expr);
            this.viewFields = translator.TranslateViewFields(expr);
            return this;
        }

        public IQuery ViewFields(string existingViewFields, Expression<Func<ListItem, object>> expr)
        {
            var lambda = this.createArrayExpression(expr);
            return ViewFields(existingViewFields, lambda);
        }

        public IQuery ViewFields(string existingViewFields, Expression<Func<ListItem, object[]>> expr)
        {
            var existingExpr = this.getViewFieldsExpressionFromString(existingViewFields);
            var exprs = new List<Expression<Func<ListItem, object[]>>>(new[] { existingExpr, expr });
            var resultExpr = this.createArrayExpression(exprs);
            return this.ViewFields(resultExpr);
        }

        public IQuery ViewFields(IEnumerable<string> titles)
        {
            if (titles == null || titles.Any(t => t == null))
            {
                throw new ArgumentNullException();
            }

            return this.ViewFields(this.createExpressionFromArray(titles));
        }

        public IQuery ViewFields(string existingViewFields, IEnumerable<string> titles)
        {
            if (titles == null || titles.Any(t => t == null))
            {
                throw new ArgumentNullException();
            }

            return this.ViewFields(existingViewFields, this.createExpressionFromArray(titles));
        }

        public IQuery Scope(ViewScope scope)
        {
            this.viewScope = scope;
            return this;
        }

        public IQuery LeftJoin(Expression<Func<ListItem, object>> expr)
        {
            var translator = translatorFactory.Create(expr);
            if (this.joins == null)
            {
                this.joins = new List<XElement>();
            }
            this.joins.Add(translator.TranslateJoin(expr, JoinType.Left));
            return this;
        }

        public IQuery InnerJoin(Expression<Func<ListItem, object>> expr)
        {
            var translator = translatorFactory.Create(expr);
            if (this.joins == null)
            {
                this.joins = new List<XElement>();
            }
            this.joins.Add(translator.TranslateJoin(expr, JoinType.Inner));
            return this;
        }

        public IQuery ProjectedField(Expression<Func<ListItem, object>> expr)
        {
            var translator = translatorFactory.Create(expr);
            if (this.projectedFields == null)
            {
                this.projectedFields = new List<XElement>();
            }
            this.projectedFields.Add(translator.TranslateProjectedField(expr));
            return this;
        }

        private Expression<Func<ListItem, object[]>> createExpressionFromArray<T>(IEnumerable<T> items)
        {
            return Expression.Lambda<Func<ListItem, object[]>>(
                Expression.NewArrayInit(
                    typeof(object),
                    (IEnumerable<Expression>) items.Select(
                        t => Expression.Call(Expression.Parameter(typeof(ListItem), ReflectionHelper.CommonParameterName),
                                             typeof(ListItem).GetMethod(ReflectionHelper.IndexerMethodName, new[] { typeof(T) }),
                                             new[] { Expression.Constant(t) })).ToArray()),
                Expression.Parameter(typeof(ListItem), ReflectionHelper.CommonParameterName));
        }

        public XElement[] ToCaml(bool includeViewTag)
        {
            var elements = new List<XElement>();

            // Return <View><Query> ... </Query></View>
            if (includeViewTag)
            {
                var viewTag = new XElement(Tags.View);

                // Set scope (e.g. RecursiveAll)
                if (this.viewScope != ViewScope.DefaultValue)
                {
                    viewTag.SetAttributeValue("Scope", this.viewScope.ToString());
                }

                // If there is a 'where', 'orderBy' or 'groupBy' defined, add a <Query> xml element to the main <View> element.
                if (this.where != null || this.orderBy != null || this.groupBy != null)
                {
                    viewTag.Add(new XElement(Tags.Query, this.where, this.orderBy, this.groupBy));
                }

                // If there are viewFields defined, add a <ViewFields> element.
                if (this.viewFields != null)
                {
                    viewTag.Add(new XElement(this.viewFields));
                }

                // If there is a rowLimit defined, add this to the main <View> element.
                if (this.rowLimit != null)
                {
                    viewTag.Add(this.rowLimit);
                }

                if (this.joins != null)
                {
                    var joinsTag = new XElement(Tags.Joins);
                    joinsTag.Add(this.joins);
                    viewTag.Add(joinsTag);
                }

                if (this.projectedFields != null)
                {
                    var projectedFieldsTag = new XElement(Tags.ProjectedFields);
                    projectedFieldsTag.Add(this.projectedFields);
                    viewTag.Add(projectedFieldsTag);
                }

                elements.Add(viewTag);
            }
            else
            {
                var queryElement = new XElement(Tags.Query);
                // Return 'raw' elements
                if (this.where != null)
                {
                    queryElement.Add(this.where);
                }
                if (this.orderBy != null)
                {
                    queryElement.Add(this.orderBy);
                }
                if (this.groupBy != null)
                {
                    queryElement.Add(this.groupBy);
                }

                if (queryElement.HasElements)
                {
                    elements.Add(queryElement);
                }

                if (this.viewFields != null)
                {
                    elements.Add(this.viewFields);
                }
                if (this.rowLimit != null)
                {
                    elements.Add(this.rowLimit);
                }

                if (this.joins != null)
                {
                    var joinsTag = new XElement(Tags.Joins);
                    joinsTag.Add(this.joins);
                    elements.Add(joinsTag);
                }

                if (this.projectedFields != null)
                {
                    var projectedFieldsTag = new XElement(Tags.ProjectedFields);
                    projectedFieldsTag.Add(this.projectedFields);
                    elements.Add(projectedFieldsTag);
                }
            }

            return elements.ToArray();
        }

        public CamlQuery ToCamlQuery()
        {
            return new CamlQuery { ViewXml = this.ToString(true) };
        }

        public override string ToString()
        {
            return this.ToString(false);
        }

        public string ToString(bool includeViewTag)
        {
            var elements = this.ToCaml(includeViewTag);
            return ConvertHelper.ConvertToString(elements);
        }

        public static implicit operator string(Query query)
        {
            return query.ToString();
        }
    }
}
