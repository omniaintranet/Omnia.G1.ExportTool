using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Fields
{
    public class LookupFieldAttribute : FieldAttribute
    {
        public const string TypeAsStringValue = "Lookup";

        /// <summary>
        ///     The value indicating whether multiple values can be
        ///     used in the lookup field.
        /// </summary>
        private bool allowMultipleValues;

        /// <summary>
        ///     The value indicating whether to return the number
        ///     of items that are related to the current list item.
        /// </summary>
        private bool countRelated;

        /// <summary>
        ///     TODO: The is relationship.
        /// </summary>
        private bool isRelationship;

        /// <summary>
        ///     The field from a non-local list to which this field is a lookup.
        /// </summary>
        private string lookupField;

        /// <summary>
        ///     The GUID of the list to use for the lookup.
        /// </summary>
        private string lookupList;

        /// <summary>
        ///     The ID of the Web site that contains the list to which this
        ///     field is a lookup.
        /// </summary>
        private string lookupWebId;

        /// <summary>
        ///     The value indicating whether to add item IDs to the
        ///     beginning of items listed in selection boxes within Edit forms.
        /// </summary>
        private bool prependId;

        /// <summary>
        ///     TODO: The primary field id.
        /// </summary>
        private string primaryFieldId;


        /// <summary>
        ///     TODO: The relationshipDeleteBehavior.
        /// </summary>
        private RelationshipDeleteBehaviorType relationshipDeleteBehavior;

        /// <summary>
        ///     The value indicating whether to allow values with
        ///     unlimited text in the lookup field.
        /// </summary>
        private bool unlimitedLengthInDocumentLibrary;



        public bool AllowMultipleValues
        {
            get
            {
                this.CheckUninitializedProperty("AllowMultipleValues");
                return allowMultipleValues;
            }
            set
            {
                this.allowMultipleValues = value;
                this.NotifyPropertyChanged("AllowMultipleValues");
            }
        }


        public bool IsRelationship
        {
            get
            {
                this.CheckUninitializedProperty("IsRelationship");
                return this.isRelationship;
            }
            set
            {
                this.isRelationship = value;
                this.NotifyPropertyChanged("IsRelationship");
            }
        }


        public string LookupField
        {
            get
            {
                this.CheckUninitializedProperty("LookupField");
                return this.lookupField;
            }
            set
            {
                this.lookupField = value;
                this.NotifyPropertyChanged("LookupField");
            }
        }


        public string LookupList
        {
            get
            {
                this.CheckUninitializedProperty("LookupList");
                return this.lookupList;
            }
            set
            {
                this.lookupList = value;
                this.NotifyPropertyChanged("LookupList");
            }
        }


        public string LookupWebId
        {
            get
            {
                this.CheckUninitializedProperty("LookupWebId");
                return this.lookupWebId;
            }
            set
            {
                this.lookupWebId = value;
                this.NotifyPropertyChanged("LookupWebId");
            }
        }


        public string PrimaryFieldId
        {
            get
            {
                this.CheckUninitializedProperty("PrimaryFieldId");
                return this.primaryFieldId;
            }
            set
            {
                this.primaryFieldId = value;
                this.NotifyPropertyChanged("PrimaryFieldId");
            }
        }

        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public bool PrependId { get; set; }

        public RelationshipDeleteBehaviorType RelationshipDeleteBehavior
        {
            get
            {
                this.CheckUninitializedProperty("RelationshipDeleteBehavior");
                return this.relationshipDeleteBehavior;
            }
            set
            {
                this.relationshipDeleteBehavior = value;
                this.NotifyPropertyChanged("RelationshipDeleteBehavior");
            }
        }

        /// <summary>
        ///     Gets or sets the list relative url.
        /// </summary>
        internal string ListRelativeUrl { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="LookupFieldAttribute"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="internalName">
        /// The internal name.
        /// </param>
        /// <param name="lookupField">
        /// The lookup field.
        /// </param>
        /// <param name="listRelativeUrl">
        /// The list relative url.
        /// </param>
        public LookupFieldAttribute(string id, string internalName, string lookupField, string listRelativeUrl)
            : base(id, internalName, TypeAsStringValue)
        {
            this.LookupField = lookupField;
            this.ListRelativeUrl = listRelativeUrl;
        }
    }
}
