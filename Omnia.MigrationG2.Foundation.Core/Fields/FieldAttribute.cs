using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Fields
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FieldAttribute : Attribute
    {


        /// <summary>
        ///     The updated properties. Contains all updated properties.
        /// </summary>
        private readonly Dictionary<string, bool> updatedProperties = new Dictionary<string, bool>();

        /// <summary>
        ///     The aggregation function.
        /// </summary>
        private string aggregationFunction;

        /// <summary>
        ///     The allow deletion.
        /// </summary>
        private bool allowDeletion;

        /// <summary>
        ///     The callout menu.
        /// </summary>
        private bool calloutMenu;

        /// <summary>
        ///     The canBeDeleted.
        /// </summary>
        private bool canBeDeleted;

        /// <summary>
        ///     The default formula.
        /// </summary>
        private string defaultFormula;

        /// <summary>
        ///     The default value.
        /// </summary>
        private string defaultValue;

        /// <summary>
        ///     The description.
        /// </summary>
        private string description;

        /// <summary>
        ///     The direction.
        /// </summary>
        private string direction;

        /// <summary>
        ///     The display size.
        /// </summary>
        private string displaySize;

        /// <summary>
        ///     The enforce unique values.
        /// </summary>
        private bool enforceUniqueValues;

        /// <summary>
        ///     The enforce entityPropertyName.
        /// </summary>
        private string entityPropertyName;

        /// <summary>
        ///     The fieldTypeKind entityPropertyName.
        /// </summary>
        private FieldType fieldTypeKind;

        /// <summary>
        ///     The group.
        /// </summary>
        private string @group;

        /// <summary>
        ///     The hidden.
        /// </summary>
        private bool hidden;

        /// <summary>
        ///     The IME mode.
        /// </summary>
        private string imeMode;

        /// <summary>
        ///     The indexed.
        /// </summary>
        private bool indexed;

        /// <summary>
        ///     The JavaScript link.
        /// </summary>
        private string jSLink;

        /// <summary>
        ///     The jump to field.
        /// </summary>
        private string jumpToField;

        /// <summary>
        ///     The link to item.
        /// </summary>
        private bool linkToItem;

        /// <summary>
        ///     The list item menu.
        /// </summary>
        private bool listItemMenu;

        /// <summary>
        ///     The no crawl.
        /// </summary>
        private bool noCrawl;

        /// <summary>
        ///     The PI attribute.
        /// </summary>
        private string piAttribute;

        /// <summary>
        ///     The PI target.
        /// </summary>
        private string piTarget;

        /// <summary>
        ///     The primary PI attribute.
        /// </summary>
        private string primaryPiAttribute;

        /// <summary>
        ///     The primary PI target.
        /// </summary>
        private string primaryPiTarget;

        /// <summary>
        ///     The push changes to lists.
        /// </summary>
        private bool pushChangesToLists;

        /// <summary>
        ///     The read only field.
        /// </summary>
        private bool readOnlyField;

        /// <summary>
        ///     The related field.
        /// </summary>
        private string relatedField;

        /// <summary>
        ///     The required.
        /// </summary>
        private bool required;

        /// <summary>
        ///     The sealed.
        /// </summary>
        private bool @sealed;

        /// <summary>
        ///     The show in display form.
        /// </summary>
        private bool showInDisplayForm;

        /// <summary>
        ///     The show in edit form.
        /// </summary>
        private bool showInEditForm;

        /// <summary>
        ///     The show in list settings.
        /// </summary>
        private bool showInListSettings;

        /// <summary>
        ///     The show in new form.
        /// </summary>
        private bool showInNewForm;

        /// <summary>
        ///     The show in version history.
        /// </summary>
        private bool showInVersionHistory;

        /// <summary>
        ///     The show in view forms.
        /// </summary>
        private bool showInViewForms;


        /// <summary>
        ///     The schemaXml
        /// </summary>
        private string schemaXml;

        /// <summary>
        ///     The title.
        /// </summary>
        private string title;

        /// <summary>
        ///     The translation xml.
        /// </summary>
        private string translationXml;

        /// <summary>
        ///     The type as string.
        /// </summary>
        private string typeAsString;

        /// <summary>
        ///     The validation formula.
        /// </summary>
        private string validationFormula;

        /// <summary>
        ///     The validation message.
        /// </summary>
        private string validationMessage;

        /// <summary>
        ///     The XPath.
        /// </summary>
        private string xpath;



        /// <summary>
        ///     Gets the updated properties. Contains all updated properties.
        /// </summary>
        internal Dictionary<string, bool> UpdatedProperties
        {
            get
            {
                return this.updatedProperties;
            }
        }


        public string DefaultValue
        {
            get
            {
                this.CheckUninitializedProperty("DefaultValue");
                return this.defaultValue;
            }
            set
            {
                this.defaultValue = value;
                this.NotifyPropertyChanged("DefaultValue");
            }
        }


        public string Description
        {
            get
            {
                this.CheckUninitializedProperty("Description");
                return this.description;
            }
            set
            {
                this.description = value;
                this.NotifyPropertyChanged("Description");
            }
        }


        public string Direction
        {
            get
            {
                this.CheckUninitializedProperty("Direction");
                return direction;
            }
            set
            {
                direction = value;
                this.NotifyPropertyChanged("Direction");
            }
        }


        public bool EnforceUniqueValues
        {
            get
            {
                this.CheckUninitializedProperty("EnforceUniqueValues");
                return this.enforceUniqueValues;
            }
            set
            {
                this.enforceUniqueValues = value;
                this.NotifyPropertyChanged("EnforceUniqueValues");
            }
        }


        public FieldType FieldTypeKind
        {
            get
            {
                this.CheckUninitializedProperty("FieldTypeKind");
                return this.fieldTypeKind;
            }
            set
            {
                this.fieldTypeKind = value;
                this.NotifyPropertyChanged("FieldTypeKind");
            }
        }



        public string Group
        {
            get
            {
                this.CheckUninitializedProperty("Group");
                return group;
            }
            set
            {
                group = value;
                this.NotifyPropertyChanged("Group");
            }
        }


        public bool Hidden
        {
            get
            {
                this.CheckUninitializedProperty("Hidden");
                return this.hidden;
            }
            set
            {
                hidden = value;
                this.NotifyPropertyChanged("Hidden");
            }
        }


        /// <summary>
        /// Gets or sets the id of the Field.
        /// </summary>
        public Guid Id { get; set; }


        public bool Indexed
        {
            get
            {
                this.CheckUninitializedProperty("Indexed");
                return this.indexed;
            }
            set
            {
                this.indexed = value;
                this.NotifyPropertyChanged("Indexed");
            }
        }


        /// <summary>
        ///     Gets or sets the internal name of the field.
        ///     Note: This property will only be written the first time. NotifyPropertyChanged is not called.
        /// </summary>
        /// <returns>
        ///     A string that specifies the internal name of the field.
        /// </returns>
        public string InternalName { get; set; }


        public string JSLink
        {
            get
            {
                this.CheckUninitializedProperty("JSLink");
                return this.jSLink;
            }
            set
            {
                this.jSLink = value;
                this.NotifyPropertyChanged("JSLink");
            }
        }



        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public ListItemMenuState ListItemMenuAllowed { get; set; }

        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public ListItemMenuState LinkToItemAllowed { get; set; }

        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public string PrimaryPITarget { get; set; }

        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public string PrimaryPIAttribute { get; set; }



        public bool ReadOnlyField
        {
            get
            {
                this.CheckUninitializedProperty("ReadOnlyField");
                return readOnlyField;
            }
            set
            {
                readOnlyField = value;
                this.NotifyPropertyChanged("ReadOnlyField");
            }
        }


        public bool Required
        {
            get
            {
                this.CheckUninitializedProperty("Required");
                return this.required;
            }
            set
            {
                this.required = value;
                this.NotifyPropertyChanged("Required");
            }
        }


        public string SchemaXml
        {
            get
            {
                this.CheckUninitializedProperty("SchemaXml");
                return this.schemaXml;
            }
            set
            {
                this.schemaXml = value;
                this.NotifyPropertyChanged("SchemaXml");
            }
        }


        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public bool ShowInDisplayForm { get; set; }


        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public bool ShowInEditForm { get; set; }


        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public bool ShowInListSettings { get; set; }

        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public bool ShowInNewForm { get; set; }

        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public bool ShowInVersionHistory { get; set; }




        public string StaticName { get; set; }

        /// <summary>
        /// TODO: Not Supported yet
        /// </summary>
        public bool Sealed { get; set; }

        /// <summary>
        /// TODO: Not Supported yet
        /// </summary>
        public string PITarget { get; set; }

        /// <summary>
        /// TODO: Not Supported yet
        /// </summary>
        public string PIAttribute { get; set; }

        /// <summary>
        /// TODO: Not Supported yet
        /// </summary>
        public bool AllowDeletion { get; set; }


        public string Title
        {
            get
            {
                this.CheckUninitializedProperty("Title");
                return this.title;
            }
            set
            {
                this.title = value;
                this.NotifyPropertyChanged("Title");
            }
        }


        public string TypeAsString
        {
            get
            {
                this.CheckUninitializedProperty("TypeAsString");
                return this.typeAsString;
            }
            set
            {
                this.typeAsString = value;
                this.NotifyPropertyChanged("TypeAsString");
            }
        }


        public string ValidationFormula
        {
            get
            {
                this.CheckUninitializedProperty("ValidationFormula");
                return validationFormula;
            }
            set
            {
                this.validationFormula = value;
                this.NotifyPropertyChanged("ValidationFormula");
            }
        }



        public string ValidationMessage
        {
            get
            {
                this.CheckUninitializedProperty("ValidationMessage");
                return this.validationMessage;
            }
            set
            {
                this.validationMessage = value;
                this.NotifyPropertyChanged("ValidationMessage");
            }
        }


        public FieldAttribute(string id, string internalName, string typeAsString)
        {
            id = id.Replace("{", string.Empty);
            id = id.Replace("}", string.Empty);

            try
            {
                Guid guid = Guid.Parse(id);
                this.Id = guid;
            }
            catch (Exception)
            {
                throw new Exception("The submitted fieldGuid is not a GUID");
            }

            this.InternalName = internalName;
            this.TypeAsString = typeAsString;
        }

        /// <summary>
        /// Notifies that the property has been changed
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        public void NotifyPropertyChanged(string propertyName)
        {
            // Set property as updated
            if (this.UpdatedProperties.All(q => q.Key != propertyName))
            {
                this.UpdatedProperties.Add(propertyName, true);
            }
        }


        public void CheckUninitializedProperty(string name)
        {
            //Todo: I dont think we need this.
        }

    }
}
