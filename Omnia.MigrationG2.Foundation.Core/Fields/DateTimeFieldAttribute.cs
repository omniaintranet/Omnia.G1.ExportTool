using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Fields
{
    public class DateTimeFieldAttribute : FieldAttribute
    {
        public const string TypeAsStringValue = "DateTime";

        #region Fields

        /// <summary>
        ///     Gets or sets the type of calendar that is used to display the field.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Microsoft.SharePoint.SPCalendarType" /> value that specifies the type of calendar.
        /// </returns>
        private CalendarType calendarType;

        /// <summary>
        ///  Gets or sets the type of calendar that is used to display the field.
        /// </summary>
        private CalendarType dateTimeCalendarType;

        /// <summary>
        ///     Gets or sets the type of date and time format that is used in the field.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:Microsoft.SharePoint.SPDateTimeFieldFormatType" /> value that specifies the type of date and time format.
        /// </returns>
        private DateTimeFieldFormatType displayFormat;

        /// <summary>
        /// The friendly display format.
        /// </summary>
        private DateTimeFieldFriendlyFormatType friendlyDisplayFormat;

        /// <summary>
        /// The JS link.
        /// </summary>
        private string jsLink;

        #endregion

        public CalendarType DateTimeCalendarType
        {
            get
            {
                this.CheckUninitializedProperty("DateTimeCalendarType");
                return this.dateTimeCalendarType;
            }
            set
            {
                this.dateTimeCalendarType = value;
                this.NotifyPropertyChanged("DateTimeCalendarType");
            }
        }


        public DateTimeFieldFormatType DisplayFormat
        {
            get
            {
                this.CheckUninitializedProperty("DisplayFormat");
                return this.displayFormat;
            }
            set
            {
                this.displayFormat = value;
                this.NotifyPropertyChanged("DisplayFormat");
            }
        }


        public DateTimeFieldFriendlyFormatType FriendlyDisplayFormat
        {
            get
            {
                this.CheckUninitializedProperty("FriendlyDisplayFormat");
                return this.friendlyDisplayFormat;
            }
            set
            {
                this.friendlyDisplayFormat = value;
                this.NotifyPropertyChanged("FriendlyDisplayFormat");
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeFieldAttribute"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public DateTimeFieldAttribute(string id, string internalName)
            : base(id, internalName, TypeAsStringValue)
        {
        }



    }
}
