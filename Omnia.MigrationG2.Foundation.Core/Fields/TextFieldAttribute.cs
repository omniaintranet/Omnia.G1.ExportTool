using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Fields
{
    public class TextFieldAttribute : FieldAttribute
    {
        public const string TypeAsStringValue = "Text";

        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public string IMEMode { get; set; }


        /// <summary>
        /// The max length.
        /// </summary>
        private int maxLength;

        public int MaxLength
        {
            get
            {
                base.CheckUninitializedProperty("MaxLength");
                return this.maxLength;
            }
            set
            {
                this.maxLength = value;
                this.NotifyPropertyChanged("MaxLength");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFieldAttribute"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="internalName">
        /// The internal name.
        /// </param>
        public TextFieldAttribute(string id, string internalName)
            : base(id, internalName, TypeAsStringValue)
        {
        }


    }
}
