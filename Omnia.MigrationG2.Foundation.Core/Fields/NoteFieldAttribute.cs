using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Fields
{
    public class NoteFieldAttribute : FieldAttribute
    {
        public const string TypeAsStringValue = "Note";

        /// <summary>
        ///     Gets or sets a value indicating whether hyperlinks can be used
        ///     in the field.
        /// </summary>
        private bool allowHyperlink;

        /// <summary>
        ///     Gets or sets a value indicating whether to append changes to
        ///     the existing text.
        /// </summary>
        private bool appendOnly;

        /// <summary>
        ///     The maximum number of differences that Windows SharePoint Services
        ///     allows between wiki page versions before it changes to a faster differencing
        ///     algorithm to prevent slow page loading.
        /// </summary>
        private int differencingLimit;

        /// <summary>
        ///     A value indicating whether the multiline text field can
        ///     be filtered.
        /// </summary>
        private bool isolateStyles;

        /// <summary>
        ///     The number of lines to display in the field.
        /// </summary>
        private int numberOfLines;

        /// <summary>
        ///     A value indicating whether this text field is in
        ///     restricted mode.
        /// </summary>
        private bool restrictedMode;

        /// <summary>
        ///     A value indicating whether rich text formatting
        ///     can be used in the field.
        /// </summary>
        private bool richText;

        /// <summary>
        ///     Gets or sets a value indicating whether to allow unlimited field
        ///     length in document libraries.
        /// </summary>
        private bool unlimitedLengthInDocumentLibrary;



        public bool AllowHyperlink
        {
            get
            {
                this.CheckUninitializedProperty("AllowHyperlink");
                return this.allowHyperlink;
            }
            set
            {
                this.allowHyperlink = value;
                this.NotifyPropertyChanged("AllowHyperlink");
            }
        }


        public bool AppendOnly
        {
            get
            {
                this.CheckUninitializedProperty("AppendOnly");
                return this.appendOnly;
            }
            set
            {
                this.appendOnly = value;
                this.NotifyPropertyChanged("AppendOnly");
            }
        }



        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public string DisplaySize { get; set; }

        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public bool IsolateStyles { get; set; }



        public int NumberOfLines
        {
            get
            {
                this.CheckUninitializedProperty("NumberOfLines");
                return this.numberOfLines;
            }
            set
            {
                this.numberOfLines = value;
                this.NotifyPropertyChanged("NumberOfLines");
            }
        }


        public bool RestrictedMode
        {
            get
            {
                this.CheckUninitializedProperty("RestrictedMode");
                return this.restrictedMode;
            }
            set
            {
                this.restrictedMode = value;
                this.NotifyPropertyChanged("RestrictedMode");
            }
        }


        public bool RichText
        {
            get
            {
                this.CheckUninitializedProperty("RichText");
                return this.richText;
            }
            set
            {
                this.richText = value;
            }
        }

        public bool UnlimitedLengthInDocumentLibrary
        {
            get
            {
                this.CheckUninitializedProperty("UnlimitedLengthInDocumentLibrary");
                return this.unlimitedLengthInDocumentLibrary;
            }
            set
            {
                this.unlimitedLengthInDocumentLibrary = value;
                this.NotifyPropertyChanged("UnlimitedLengthInDocumentLibrary");
            }
        }


        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public RichTextMode RichTextMode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteFieldAttribute"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="internalName">
        /// The internal name.
        /// </param>
        public NoteFieldAttribute(string id, string internalName)
            : base(id, internalName, TypeAsStringValue)
        {
        }




    }
}
