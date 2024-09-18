using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Utilities;
using Omnia.MigrationG2.Foundation.Core.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Utilities
{
    /// <summary>
    /// SharePointUtils
    /// </summary>
    public class SharePointUtils
    {
        /// <summary>
        /// Gets the web timezone information.
        /// </summary>
        /// <param name="web">The web.</param>
        /// <returns></returns>
        public static TimeZoneInfo GetWebTimezoneInfo(Web web)
        {
            return CommonUtils.GetTimeZoneInfo(web.RegionalSettings.TimeZone.Description);
        }

        /// <summary>
        /// Gets the date field value.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        /// <param name="fieldValue">The field value.</param>
        /// <returns></returns>
        public static DateTime GetDateFieldValue(AppClientContext ctx, object fieldValue)
        {
            DateTime dateFieldValue = (DateTime)fieldValue;
            ClientResult<string> cr = Utility.FormatDateTime(ctx, ctx.Web, dateFieldValue, DateTimeFormat.DateTime);
            ctx.ExecuteQuery();
            //DateTime webdt = DateTime.Parse(cr.Value.ToString());
            DateTime webdt = GetFieldDatetimebyTimeZone(ctx, dateFieldValue, cr.Value.ToString());
            return webdt;
        }

        /// <summary>
		/// Gets the date field value.
		/// </summary>
		/// <param name="webTimezoneInfo">The web timezone information.</param>
		/// <param name="fieldValue">The field value.</param>
		/// <returns></returns>
		public static DateTime GetDateFieldValue(TimeZoneInfo webTimezoneInfo, object fieldValue)
        {
            DateTime dateFieldValue = (DateTime)fieldValue;
            return TimeZoneInfo.ConvertTimeFromUtc(dateFieldValue, webTimezoneInfo);
        }

        /// <summary>
		/// Gets web date time by format value.
		/// </summary>
		/// <param name="ctx">The CTX.</param>
		/// <param name="dateFieldValue">Date time field value.</param>
		/// <param name="compareVal">Client context value.</param>
		/// <returns></returns>
		private static DateTime GetFieldDatetimebyTimeZone(AppClientContext ctx, DateTime dateFieldValue, string compareVal)
        {
            dateFieldValue = dateFieldValue.ToUniversalTime();
            DateTime ret = dateFieldValue;

            if (DateTime.TryParse(compareVal, out ret))
                return ret;

            Microsoft.SharePoint.Client.TimeZone tz = ctx.Web.RegionalSettings.TimeZone;
            ctx.Load(tz);
            ctx.ExecuteQuery();
            //Cannot compare by tz description because it is different by the various languages (web & SP sv)
            string utcValue = tz.Description.Substring(0, tz.Description.LastIndexOf(')') + 1);
            var tzGroups = TimeZoneInfo.GetSystemTimeZones().Where(t => t.DisplayName.Contains(utcValue)).ToList();
            foreach (var tzInfo in tzGroups)
            {
                try
                {
                    ret = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateFieldValue, tzInfo.Id);
                    if (compareVal.Contains(ret.ToString("HH:mm"))//24 hours
                        || compareVal.Contains(ret.ToString("HH:mm:ss"))
                        || compareVal.Contains(ret.ToString("hh:mm"))
                        || compareVal.Contains(ret.ToString("hh:mm:ss")))
                    {
                        break;
                    }
                }
                catch { }
            }

            return ret;
        }
    }
}
