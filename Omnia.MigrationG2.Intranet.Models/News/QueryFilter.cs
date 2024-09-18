using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.News
{
    public class QueryFilter
    {
        public string FieldName { get; set; }
        public QueryFilterType FilterType { get; set; }
        public string FromDateValue { get; set; }
        public string HiddenFieldName { get; set; }
        public bool IncludeChildTerms { get; set; }
        public bool IncludeEmpty { get; set; }
        public QueryFilterOperator Operator { get; set; }
        public string TermSetId { get; set; }
        public string ToDateValue { get; set; }
        public string TypeAsString { get; set; }
        public string Value { get; set; }
    }
}
