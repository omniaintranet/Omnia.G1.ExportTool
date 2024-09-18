using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Fields
{
    public class NumberFieldAttribute : FieldAttribute
    {
        public const string TypeAsStringValue = "Number";

        #region Fields


        /// <summary>
        ///     The maximum value for the field.
        /// </summary>
        private double maximumValue;

        /// <summary>
        ///     The minimum value for the field.
        /// </summary>
        private double minimumValue;

        /// <summary>
        ///     A value that specifies whether to render the field as
        ///     a percentage.
        /// </summary>
        private bool showAsPercentage;

        #endregion

        /// <summary>
        /// TODO: Not implemented yet
        /// </summary>
        public bool ShowAsPercentage { get; set; }


        public double MaximumValue
        {
            get
            {
                base.CheckUninitializedProperty("MaximumValue");
                return this.maximumValue;
            }
            set
            {
                this.maximumValue = value;
                this.NotifyPropertyChanged("MaximumValue");
            }
        }


        public double MinimumValue
        {
            get
            {
                base.CheckUninitializedProperty("MinimumValue");
                return this.minimumValue;
            }
            set
            {
                this.minimumValue = value;
                this.NotifyPropertyChanged("MinimumValue");
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NumberFieldAttribute"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="internalName">
        /// The internal name.
        /// </param>
        public NumberFieldAttribute(string id, string internalName)
            : base(id, internalName, TypeAsStringValue)
        {
        }
    }
}
