using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Fields
{
    public class ManagedMetadataFieldAttribute : FieldAttribute
    {
        public const string TypeSingleAsStringValue = "TaxonomyFieldType";
        public const string TypeMultiAsStringValue = "TaxonomyFieldTypeMulti";

        private Guid sspId = Guid.Empty;
        private string termStoreName;
        private Guid termGroupId = Guid.Empty;
        private string termGroupName;
        private Guid termSetId = Guid.Empty;
        private string termSetName;
        private Guid anchorId = Guid.Empty;
        private string anchorName;
        private string targetTemplate;
        private bool isOpenForTermCreation;
        /// <summary>
        ///     The value indicating whether multiple values can be used
        /// </summary>
        private bool allowMultipleValues;

        private string showField;
        /// <summary>
        /// Gets or sets the Term Store Id.
        /// </summary>
        /// <value>
        /// The SSP identifier.
        /// </value>
        public Guid SspId
        {
            get
            {
                this.CheckUninitializedProperty("SspId");
                return this.sspId;
            }
            set
            {
                this.sspId = value;
                this.NotifyPropertyChanged("SspId");
            }
        }

        /// <summary>
        /// Gets or sets the name of the term store.
        /// </summary>
        /// <value>
        /// The name of the term store.
        /// </value>
        public string TermStoreName
        {
            get
            {
                this.CheckUninitializedProperty("TermStoreName");
                return this.termStoreName;
            }
            set
            {
                this.termStoreName = value;
                this.NotifyPropertyChanged("TermStoreName");
            }
        }

        public Guid TermGroupId
        {
            get
            {
                this.CheckUninitializedProperty("TermGroupId");
                return this.termGroupId;
            }
            set
            {
                this.termGroupId = value;
                this.NotifyPropertyChanged("TermGroupId");
            }
        }

        public string TermGroupName
        {
            get
            {
                this.CheckUninitializedProperty("TermGroupName");
                return this.termGroupName;
            }
            set
            {
                this.termGroupName = value;
                this.NotifyPropertyChanged("TermGroupName");
            }
        }

        /// <summary>
        /// Gets or sets the term set Id.
        /// </summary>
        /// <value>
        /// The term set identifier.
        /// </value>
        public Guid TermSetId
        {
            get
            {
                this.CheckUninitializedProperty("TermSetId");
                return this.termSetId;
            }
            set
            {
                this.termSetId = value;
                this.NotifyPropertyChanged("TermSetId");
            }
        }


        /// <summary>
        /// Gets or sets the name of the term set.
        /// </summary>
        /// <value>
        /// The name of the term set.
        /// </value>
        public string TermSetName
        {
            get
            {
                this.CheckUninitializedProperty("TermSetName");
                return this.termSetName;
            }
            set
            {
                this.termSetName = value;
                this.NotifyPropertyChanged("TermSetName");
            }
        }

        /// <summary>
        /// Gets or sets the anchor identifier.
        /// </summary>
        /// <value>
        /// The anchor identifier.
        /// </value>
        public Guid AnchorId
        {
            get
            {
                this.CheckUninitializedProperty("AnchorId");
                return this.anchorId;
            }
            set
            {
                this.anchorId = value;
                this.NotifyPropertyChanged("AnchorId");
            }
        }

        /// <summary>
        /// Gets or sets the name of the anchor.
        /// </summary>
        /// <value>
        /// The name of the anchor.
        /// </value>
        public string AnchorName
        {
            get
            {
                this.CheckUninitializedProperty("AnchorName");
                return this.anchorName;
            }
            set
            {
                this.anchorName = value;
                this.NotifyPropertyChanged("AnchorName");
            }
        }
        /// <summary>
        /// Gets or sets the target template.
        /// </summary>
        /// <value>
        /// The target template.
        /// </value>
        public string TargetTemplate
        {
            get
            {
                this.CheckUninitializedProperty("TargetTemplate");
                return this.targetTemplate;
            }
            set
            {
                this.targetTemplate = value;
                this.NotifyPropertyChanged("TargetTemplate");
            }
        }
        /// <summary>
        /// Gets or sets the show field.
        /// </summary>
        /// <value>
        /// The show field.
        /// </value>
        public string ShowField
        {
            get
            {
                return this.showField;
            }
            set
            {
                this.showField = value;
            }
        }

        /// <summary>
        /// Get Or Set Allow Multiple Values (should use the constructor parameter instead)
        /// </summary>
        /// <value>
        /// <c>true</c> if [allow multiple values]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowMultipleValues
        {
            get
            {
                this.CheckUninitializedProperty("AllowMultipleValues");
                return this.allowMultipleValues;
            }
            set
            {
                this.allowMultipleValues = value;
                this.NotifyPropertyChanged("AllowMultipleValues");
            }
        }

        /// <summary>
        /// Get Or Set Open values (should use the constructor parameter instead)
        /// </summary>
        /// <value>
        /// <c>true</c> if [allow user input value]; otherwise, <c>false</c>.
        /// </value>
        public bool Open
        {
            get
            {
                this.CheckUninitializedProperty("Open");
                return this.isOpenForTermCreation;
            }
            set
            {
                this.isOpenForTermCreation = value;
                this.NotifyPropertyChanged("Open");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupFieldAttribute" /> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="internalName">The internal name.</param>
        public ManagedMetadataFieldAttribute(string id, string internalName, bool isMulti,
            string termStoreIdOrName = null, string termGroupIdOrName = null, string termSetIdOrName = null, string anchorIdOrName = null)
            : base(id, internalName, TypeMultiAsStringValue)
        {
            Guid parsedGuid = Guid.Empty;
            if (!string.IsNullOrEmpty(termStoreIdOrName))
            {
                if (Guid.TryParse(termStoreIdOrName, out parsedGuid))
                {
                    this.SspId = parsedGuid;
                }
                else
                {
                    this.TermStoreName = termStoreIdOrName;
                }
            }

            if (!string.IsNullOrEmpty(termGroupIdOrName))
            {
                if (Guid.TryParse(termGroupIdOrName, out parsedGuid))
                {
                    this.TermGroupId = parsedGuid;
                }
                else
                {
                    this.TermGroupName = termGroupIdOrName;
                }
            }

            if (!string.IsNullOrEmpty(termSetIdOrName))
            {
                if (Guid.TryParse(termSetIdOrName, out parsedGuid))
                {
                    this.TermSetId = parsedGuid;
                }
                else
                {
                    this.TermSetName = termSetIdOrName;
                }
            }

            if (!string.IsNullOrEmpty(anchorIdOrName))
            {
                if (Guid.TryParse(anchorIdOrName, out parsedGuid))
                {
                    this.AnchorId = parsedGuid;
                }
                else
                {
                    this.AnchorName = anchorIdOrName;
                }
            }

            this.TypeAsString = !isMulti ? TypeSingleAsStringValue : TypeMultiAsStringValue;
            this.AllowMultipleValues = isMulti;
        }
    }
}
