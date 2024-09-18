using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Fields
{
    public class UserFieldAttribute : LookupFieldAttribute
    {
        public const string TypeSingleAsStringValue = "User";
        public const string TypeMultiAsStringValue = "UserMulti";

        /// <summary>
        ///     A value indicating whether to display the name of the user who created or last modified the field.
        /// </summary>
        private bool allowDisplay;

        /// <summary>
        ///     The value indicating whether multiple values can be
        ///     used in the user field.
        /// </summary>
        private bool allowMultipleValues;

        /// <summary>
        /// The JS link.
        /// </summary>
        private string jsLink;

        /// <summary>
        ///     A value indicating whether presence is enabled on the field to display user names and e-mail addresses.
        /// </summary>
        private bool presence;

        /// <summary>
        /// The selection group.
        /// </summary>
        private int selectionGroup;

        /// <summary>
        ///     A value indicating whether only individuals or both individuals and groups can be selected as field values in forms for creating or editing list items.
        /// </summary>
        private FieldUserSelectionMode selectionMode;


        public bool AllowDisplay
        {
            get
            {
                this.CheckUninitializedProperty("AllowDisplay");
                return this.allowDisplay;
            }
            set
            {
                this.allowDisplay = value;
                this.NotifyPropertyChanged("AllowDisplay");
            }
        }


        public bool Presence
        {
            get
            {
                this.CheckUninitializedProperty("Presence");
                return this.presence;
            }
            set
            {
                this.presence = value;
                this.NotifyPropertyChanged("Presence");
            }
        }

        /// <summary>
        /// Gets or sets the UserSelectionScope value.
        /// </summary>
        /// <value>
        /// The selection group.
        /// </value>
        public int SelectionGroup
        {
            get
            {
                this.CheckUninitializedProperty("SelectionGroup");
                return this.selectionGroup;
            }
            set
            {
                this.selectionGroup = value;
                this.NotifyPropertyChanged("SelectionGroup");
            }
        }

        /// <summary>
        /// Gets or sets the UserSelectionMode value.
        /// </summary>
        /// <value>
        /// The selection mode.
        /// </value>
        public FieldUserSelectionMode SelectionMode
        {
            get
            {
                this.CheckUninitializedProperty("SelectionMode");
                return this.selectionMode;
            }
            set
            {
                this.selectionMode = value;
                this.NotifyPropertyChanged("SelectionMode");
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
        /// Initializes a new instance of the <see cref="UserFieldAttribute"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="internalName">
        /// The internal name.
        /// </param>
        public UserFieldAttribute(string id, string internalName, bool allowMulti = false, string showField = "ImnName")
            : base(id, internalName, showField, "UserInfo")
        {
            this.AllowMultipleValues = allowMulti;
            this.TypeAsString = allowMulti ? TypeMultiAsStringValue : TypeSingleAsStringValue;
        }

    }
}
