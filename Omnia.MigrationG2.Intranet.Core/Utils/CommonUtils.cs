using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Omnia.MigrationG2.Intranet.Core.Utils
{
    public class CommonUtils
    {
        public static string GetSetASDefaultConfigurationRegion(string navigationSourceUrl)
        {
            return Constants.Configurations.Regions.SetASDefaultPage + "_" + navigationSourceUrl.ToLower();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="absoluteUrl"></param>
        /// <returns></returns>
        public static string GetHostUrl(string absoluteUrl)
        {
            if (string.IsNullOrEmpty(absoluteUrl))
                return absoluteUrl;

            var urlParts = absoluteUrl.Split('/');
            return string.Join("/", urlParts.Take(3));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EnsureDisplayTermString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            //Convert the fullwidth ampersand to small ampersand
            return Regex.Replace(value, "\uff06", "\u0026");
        }
    }
}
