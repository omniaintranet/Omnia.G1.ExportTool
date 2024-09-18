using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Fields
{
    /// <summary>
    ///     The field ref attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class FieldRefAttribute : Attribute
    {
        // TODO: lägg i interface?
        #region Fields

        /// <summary>
        ///     The updated properties.
        /// </summary>
        internal Dictionary<string, bool> updatedProperties = new Dictionary<string, bool>();

        /// <summary>
        ///     The aggregation function for the field. Used to promote and
        ///     demote properties in XML documents.
        /// </summary>
        private string aggregationFunction;

        // TODO: ska denna finnas med?

        /// <summary>
        ///     A string in Collaborative Application Markup Language (CAML)
        ///     that defines custom field properties.
        /// </summary>
        private string customization;

        /// <summary>
        ///     The display name for the field reference.
        /// </summary>
        private string displayName;


        /// <summary>
        /// TODO: The field
        /// </summary>
        private FieldBase field;

        /// <summary>
        ///     A value that specifies whether the field is displayed
        ///     in forms that can be edited.
        /// </summary>
        private bool hidden;

        /// <summary>
        ///     The order of the field on the content type.
        ///     Note: Default value is -1 because we want to be able to set it to 0.
        /// </summary>
        private int ordinal = -1;

        /// <summary>
        ///     The attribute of the processing instruction (specified by the
        ///     Microsoft.SharePoint.SPFieldLink.PITarget property) to use for document property
        ///     promotion and demotion.
        /// </summary>
        private string piAttribute;

        /// <summary>
        ///     The name of the processing instruction in an XML document of
        ///     this content type, which can then be used to promote and demote property
        ///     values.
        /// </summary>
        private string piTarget;

        /// <summary>
        ///     The attribute of the primary processing instruction (specified
        ///     by the Microsoft.SharePoint.SPFieldLink.PrimaryPITarget property) that is
        ///     used to promote and demote the document property.
        /// </summary>
        private string primaryPiAttribute;

        /// <summary>
        ///     The name of the primary processing instruction in an XML document
        ///     of the specified content type, which is then used to promote and demote the
        ///     document property.
        /// </summary>
        private string primaryPiTarget;

        /// <summary>
        ///     A value that indicates whether the Microsoft.SharePoint.SPFieldLink
        ///     object can be modified.
        /// </summary>
        private bool readOnlyField;

        /// <summary>
        ///     A value that indicates whether the column that is referenced
        ///     must have a value.
        /// </summary>
        private bool required;

        /// <summary>
        ///     A value indicating whether show in display form.
        /// </summary>
        private bool showInDisplayForm;

        /// <summary>
        ///     An XML Path Language (XPath) expression that represents the
        ///     location of a property within an XML document of this content type.
        /// </summary>
        private string xpath;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldRefAttribute"/> class.
        /// </summary>
        /// <param name="fieldType">Type of the field.</param>
        /// <param name="index">The index.</param>
        public FieldRefAttribute(Type fieldType, int index = -1)
        {
            var fieldInstance = Activator.CreateInstance(fieldType) as FieldBase;
            if (fieldInstance != null)
            {
                this.field = fieldInstance;
            }
            this.Index = index;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int Index { get; set; }

        /// <summary>
        ///     Gets or sets the aggregation function for the field. Used to promote and
        ///     demote properties in XML documents.
        /// </summary>
        /// <returns>
        ///     A System.String value that contains the aggregation function. The default
        ///     value is null.
        /// </returns>
        public string AggregationFunction
        {
            get
            {
                return this.aggregationFunction;
            }

            set
            {
                this.aggregationFunction = value;
                this.NotifyPropertyChanged("AggregationFunction");
            }
        }

        /// <summary>
        ///     Gets or sets a string in Collaborative Application Markup Language (CAML)
        ///     that defines custom field properties.
        /// </summary>
        /// <returns>
        ///     A string in CAML that contains a set of Field elements that define properties
        ///     of the field.
        /// </returns>
        public string Customization
        {
            get
            {
                return this.customization;
            }

            set
            {
                this.customization = value;
                this.NotifyPropertyChanged("Customization");
            }
        }

        /// <summary>
        ///     Gets or sets the display name for the field reference.
        /// </summary>
        /// <returns>
        ///     A System.String object that contains the display name of the field reference.
        /// </returns>
        public string DisplayName
        {
            get
            {
                return this.displayName;
            }

            set
            {
                this.displayName = value;
                this.NotifyPropertyChanged("DisplayName");
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the field is displayed
        ///     in forms that can be edited.
        /// </summary>
        /// <returns>
        ///     true if the field is omitted; otherwise, false. The default value is false.
        /// </returns>
        public bool Hidden
        {
            get
            {
                return this.hidden;
            }

            set
            {
                this.hidden = value;
                this.NotifyPropertyChanged("Hidden");
            }
        }

        /// <summary>
        ///     Gets or sets the sort order.
        /// </summary>
        /// <returns>
        ///     TODO...
        /// </returns>
        public int Ordinal
        {
            get
            {
                return this.ordinal;
            }

            set
            {
                this.ordinal = value;
                this.NotifyPropertyChanged("Ordinal");
            }
        }

        /// <summary>
        ///     Gets or sets the attribute of the processing instruction (specified by the
        ///     Microsoft.SharePoint.SPFieldLink.PITarget property) to use for document property
        ///     promotion and demotion.
        /// </summary>
        /// <returns>
        ///     A string that contains the attribute name.
        /// </returns>
        public string PIAttribute
        {
            get
            {
                return this.piAttribute;
            }

            set
            {
                this.piAttribute = value;
                this.NotifyPropertyChanged("PIAttribute");
            }
        }

        /// <summary>
        ///     Gets or sets the name of the processing instruction in an XML document of
        ///     this content type, which can then be used to promote and demote property
        ///     values.
        /// </summary>
        /// <returns>
        ///     A string that contains the processing instruction name.
        /// </returns>
        public string PITarget
        {
            get
            {
                return this.piTarget;
            }

            set
            {
                this.piTarget = value;
                this.NotifyPropertyChanged("PITarget");
            }
        }

        /// <summary>
        ///     Gets or sets the attribute of the primary processing instruction (specified
        ///     by the Microsoft.SharePoint.SPFieldLink.PrimaryPITarget property) that is
        ///     used to promote and demote the document property.
        /// </summary>
        /// <returns>
        ///     A string that contains the attribute name.
        /// </returns>
        public string PrimaryPIAttribute
        {
            get
            {
                return this.primaryPiAttribute;
            }

            set
            {
                this.primaryPiAttribute = value;
                this.NotifyPropertyChanged("PrimaryPIAttribute");
            }
        }

        /// <summary>
        ///     Gets or sets the name of the primary processing instruction in an XML document
        ///     of the specified content type, which is then used to promote and demote the
        ///     document property.
        /// </summary>
        /// <returns>
        ///     A string that contains the processing instruction name.
        /// </returns>
        public string PrimaryPITarget
        {
            get
            {
                return this.primaryPiTarget;
            }

            set
            {
                this.primaryPiTarget = value;
                this.NotifyPropertyChanged("PrimaryPITarget");
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the Microsoft.SharePoint.SPFieldLink
        ///     object can be modified.
        /// </summary>
        /// <returns>
        ///     true if the field reference is read-only; otherwise, false. The default is
        ///     false.
        /// </returns>
        public bool ReadOnlyField
        {
            get
            {
                return this.readOnlyField;
            }

            set
            {
                this.readOnlyField = value;
                this.NotifyPropertyChanged("ReadOnlyField");
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the column that is referenced
        ///     must have a value.
        /// </summary>
        /// <returns>
        ///     true if the column that is referenced must have a value; otherwise, false.
        /// </returns>
        public bool Required
        {
            get
            {
                return this.required;
            }

            set
            {
                this.required = value;
                this.NotifyPropertyChanged("Required");
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether show in display form.
        /// </summary>
        /// <returns>
        ///     Returns System.Boolean.
        /// </returns>
        public bool ShowInDisplayForm
        {
            get
            {
                return this.showInDisplayForm;
            }

            set
            {
                this.showInDisplayForm = value;
                this.NotifyPropertyChanged("ShowInDisplayForm");
            }
        }

        /// <summary>
        ///     Gets or sets an XML Path Language (XPath) expression that represents the
        ///     location of a property within an XML document of this content type.
        /// </summary>
        /// <returns>
        ///     A string that contains an XPath expression. The default value is null.
        /// </returns>
        public string XPath
        {
            get
            {
                return this.xpath;
            }

            set
            {
                this.xpath = value;
                this.NotifyPropertyChanged("XPath");
            }
        }

        /// <summary>
        /// Gets the field.
        /// </summary>
        /// <value>
        /// The field.
        /// </value>
        public FieldBase Field
        {
            get
            {
                return this.field;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     If the field can be updated, e.g Modified and Created is updateable=false
        /// TODO: ska denna finnas?
        /// </summary>
        internal bool IsUpdateable { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Notifies that the property has been changed
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        public void NotifyPropertyChanged(string propertyName)
        {
            // Set property as updated
            if (this.updatedProperties.All(q => q.Key != propertyName))
            {
                this.updatedProperties.Add(propertyName, true);
            }
        }

        #endregion
    }
}
