using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Fields
{
    public class BooleanFieldAttribute : FieldAttribute
    {
        public const string TypeAsStringValue = "Boolean";

        /// <summary>
        ///     The internal name of the field to which to jump in a branched survey when the No option of the Boolean field is selected.
        /// </summary>
        private string jumpToNoField;

        /// <summary>
        ///     The internal name of the field to which to jump in a branched survey when the Yes option of the Boolean field is selected.
        /// </summary>
        private string jumpToYesField;

        /// <summary>
        ///     Gets or sets the internal name of the field to which to jump in a branched survey when the No option of the Boolean field is selected.
        /// </summary>
        /// <returns>
        ///     A string that contains the internal name of the field to which to jump.
        /// </returns>
        public string JumpToNoField
        {
            get
            {
                return this.jumpToNoField;
            }

            set
            {
                this.jumpToNoField = value;
                this.NotifyPropertyChanged("JumpToNoField");
            }
        }

        /// <summary>
        ///     Gets or sets the internal name of the field to which to jump in a branched survey when the Yes option of the Boolean field is selected.
        /// </summary>
        /// <returns>
        ///     A string that contains the internal name of the field to which to jump.
        /// </returns>
        public string JumpToYesField
        {
            get
            {
                return this.jumpToYesField;
            }

            set
            {
                this.jumpToYesField = value;
                this.NotifyPropertyChanged("JumpToYesField");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanFieldAttribute"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public BooleanFieldAttribute(string id, string internalName)
            : base(id, internalName, TypeAsStringValue)
        {
        }

    }
}
