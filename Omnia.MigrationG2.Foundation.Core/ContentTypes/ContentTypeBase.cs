using Microsoft.SharePoint.Client;
using Omnia.MigrationG2.Foundation.Core.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.ContentTypes
{
    public abstract class ContentTypeBase
    {
        private string _name = null;
        private string _group = null;
        private bool? _isBuiltIn = null;
        private string _idAsString = null;
        private string _description = null;

        /// <summary>
        /// Gets the identifier as string.
        /// </summary>
        /// <value>
        /// The identifier as string.
        /// </value>
        public string IdAsString
        {
            get
            {
                if (string.IsNullOrEmpty(this._idAsString))
                {
                    this._idAsString = GetContentTypeIdAsString();
                }
                return this._idAsString;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    this._name = this.GetContentTypeAttributeProperty("Name") as string;
                }
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>
        /// The group.
        /// </value>
        public string Group
        {
            get
            {
                if (string.IsNullOrEmpty(_group))
                {
                    this._group = this.GetContentTypeAttributeProperty("Group") as string;
                }
                return this._group;
            }
            set
            {
                this._group = value;
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get
            {
                if (string.IsNullOrEmpty(_description))
                {
                    this._description = this.GetContentTypeAttributeProperty("Description") as string;
                }
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is built in.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is built in; otherwise, <c>false</c>.
        /// </value>
        public bool IsBuiltIn
        {
            get
            {
                if (_isBuiltIn == null)
                {
                    var isBuiltInValue = this.GetContentTypeAttributeProperty("IsBuiltIn");
                    if (isBuiltInValue == null)
                    {
                        _isBuiltIn = false;
                    }
                    else
                    {
                        _isBuiltIn = Convert.ToBoolean(isBuiltInValue);
                    }
                }
                return _isBuiltIn.Value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the type of the sp content.
        /// </summary>
        /// <value>
        /// The type of the sp content.
        /// </value>
        public ContentType SPContentType { get; set; }

        /// <summary>
        /// Gets the fields specified in ContentType using FieldRefAttribute.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FieldBase> GetFields()
        {
            var result = new List<FieldBase>();
            var currentType = this.GetType();
            var properties = currentType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                var fieldAttribute = prop.GetCustomAttribute<FieldRefAttribute>();
                if (fieldAttribute != null)
                    result.Add(fieldAttribute.Field);
            }
            return result;
        }

        /// <summary>
        /// Gets the field refs specified in the ContentType.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FieldRefAttribute> GetFieldRefs()
        {
            var result = new List<FieldRefAttribute>();
            var currentType = this.GetType();
            var properties = currentType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                var fieldAttribute = prop.GetCustomAttribute<FieldRefAttribute>();
                if (fieldAttribute != null)
                    result.Add(fieldAttribute);
            }
            return result;
        }

        private object GetContentTypeAttributeProperty(string propertyName)
        {
            var propertyValue = GetAttributeProperty<ContentTypeAttribute>(propertyName);
            return propertyValue == null ? GetAttributeProperty<ContentTypeRefAttribute>(propertyName) : propertyValue;
        }

        private object GetAttributeProperty<T>(string propertyName) where T : Attribute
        {
            var typeAttribute = this.GetType().GetCustomAttribute<T>(false);
            PropertyInfo property = null;
            if (typeAttribute != null)
            {
                property = typeAttribute.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(q => q.Name == propertyName);
            }
            return property == null ? null : property.GetValue(typeAttribute);
        }

        private string PrependContentTypeId(string contentTypeIdAsString, string parentIdString)
        {
            if (!contentTypeIdAsString.ToLower().Contains("x") && !string.IsNullOrEmpty(parentIdString))
            {
                contentTypeIdAsString = string.Format("{0}00{1}", parentIdString, contentTypeIdAsString);
            }
            return contentTypeIdAsString;
        }

        /// <summary>
        /// Gets the content type id as string.
        /// </summary>
        private string GetContentTypeIdAsString()
        {
            const string IdProperty = "Id";
            var contentTypeIdAsString = Convert.ToString(this.GetContentTypeAttributeProperty(IdProperty));
            if (this.IsBuiltIn)
                return contentTypeIdAsString;
            var baseType = this.GetType().BaseType;
            if (baseType != null && baseType.IsSubclassOf(typeof(ContentTypeBase)))
            {
                var baseTypeInstance = Activator.CreateInstance(baseType) as ContentTypeBase;
                if (baseTypeInstance != null)
                {
                    var baseIdValue = baseTypeInstance.IdAsString;
                    contentTypeIdAsString = PrependContentTypeId(contentTypeIdAsString, baseIdValue);
                }
            }
            return contentTypeIdAsString;
        }

    }
}
