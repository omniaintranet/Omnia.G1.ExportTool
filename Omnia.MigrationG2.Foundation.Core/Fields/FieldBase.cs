using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Fields
{
    public class FieldBase
    {
        private string _title;
        private string _group;
        private string _description;

        /// <summary>
        /// Gets the id as string.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// TODO: TBD
        /// </exception>
        public string IdAsString
        {
            get
            {
                return this.Id.ToString();
            }
        }

        /// <summary>
        /// Gets the type as string.
        /// </summary>
        /// <value>
        /// The type as string.
        /// </value>
        public string TypeAsString
        {
            get
            {
                return this.GetFieldAttributeProperty("TypeAsString") as string;
            }
        }

        /// <summary>
        /// Gets the name of the internal.
        /// </summary>
        /// <value>
        /// The name of the internal.
        /// </value>
        public string InternalName
        {
            get
            {
                return this.GetFieldAttributeProperty("InternalName") as string;
            }
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id
        {
            get
            {
                return (Guid)this.GetFieldAttributeProperty("Id");
            }
        }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>
        /// The group.
        /// </value>
        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(_title))
                    _title = this.GetFieldAttributeProperty("Title").ToString();
                return _title;
            }
            set
            {
                _title = value;
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
                    _group = this.GetFieldAttributeProperty("Group").ToString();
                return _group;
            }
            set
            {
                _group = value;
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
                    _description = this.GetFieldAttributeProperty("Description").ToString();
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        /// <summary>
        /// The get field attribute property.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetFieldAttributeProperty(string propertyName)
        {
            var fieldAttribute = this.GetType().GetCustomAttribute<FieldAttribute>(true);

            var property = fieldAttribute.GetType()
                              .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(q => q.Name == propertyName);

            return property == null ? null : property.GetValue(fieldAttribute);
        }

        /// <summary>
        /// Occurs after a field is added.
        /// </summary>
        /// <param name="parent">
        /// The web or the list.
        /// </param>
        public virtual void OnAdded(object parent)
        {

        }

        /// <summary>
        /// Occurs after changes are made to a field.
        /// </summary>
        /// <param name="parent">
        /// The web or the list.
        /// </param>
        public virtual void OnUpdated(object parent)
        {

        }

        /// <summary>
        /// Gets or sets the sp field.
        /// </summary>
        /// <value>
        /// The sp field.
        /// </value>
        public Field SPField { get; set; }

    }
}
