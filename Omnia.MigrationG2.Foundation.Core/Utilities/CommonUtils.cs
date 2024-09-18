using Omnia.MigrationG2.Foundation.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Omnia.MigrationG2.Foundation.Core.Utilities
{
    public class CommonUtils
    {
        /// <summary>
        /// Creates MD5 hash based on string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string CreateMd5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Encodes the base64.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns></returns>
        public static string EncodeBase64(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Strips the byte order mark UTF8.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string StripByteOrderMarkUTF8(string text)
        {
            var byteOrderMarkUTF8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());

            if (text.StartsWith(byteOrderMarkUTF8))
            {
                text = text.Remove(0, byteOrderMarkUTF8.Length);
            }

            return text;
        }

        /// <summary>
        /// Decodes the base64.
        /// </summary>
        /// <param name="base64EncodedData">The base64 encoded data.</param>
        /// <returns></returns>
        public static string DecodeBase64(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string ConvertToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return char.ToUpper(input[0]) + input.Substring(1);
        }

        /// <summary>
        /// Creates MD5 hash based on byte array.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static string CreateMd5Hash(byte[] content)
        {
            byte[] computedHash = new MD5CryptoServiceProvider().ComputeHash(content);
            var sb = new StringBuilder();
            foreach (byte b in computedHash)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetStringValue(object value)
        {
            if (value == null)
                return string.Empty;
            else
                return value.ToString();
        }

        /// <summary>
        /// Gets the int value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int GetIntValue(object value)
        {
            if (value == null || value.ToString() == "")
                return 0;
            else
                return Convert.ToInt32(value);
        }

        /// <summary>
        /// Strings to bytes array.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static byte[] StringToBytesArray(string str)
        {
            byte[] result = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, result, 0, result.Length);

            return result;
        }

        /// <summary>
        /// Converts to URL friendly string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string ConvertToUrlFriendly(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            var asciiText = System.Text.Encoding.ASCII.GetString(bytes).ToLower();

            // invalid chars           
            asciiText = Regex.Replace(asciiText, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            asciiText = Regex.Replace(asciiText, @"\s+", " ").Trim();
            // hyphens   
            asciiText = Regex.Replace(asciiText, @"\s", "-");

            return asciiText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public static string EnsureSPFieldInternalName(string displayName)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(displayName);
            var asciiText = System.Text.Encoding.ASCII.GetString(bytes).ToLower();

            // invalid chars           
            asciiText = Regex.Replace(asciiText, @"[^a-z0-9\s]", "");
            // remove all spaces
            asciiText = Regex.Replace(asciiText, @"\s+", "").Trim();
            // xml encode   
            asciiText = System.Xml.XmlConvert.EncodeName(asciiText);

            return asciiText;
        }

        /// <summary>
        /// Determines whether [is URL valid] [the specified URL].
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static bool IsUrlValid(string url)
        {
            Uri uriResult;
            var result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }

        /// <summary>
        /// Removes the trailing slash.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string RemoveTrailingSlash(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            if (text[text.Length - 1] == '/')
                return text.Substring(0, text.Length - 1);
            else
                return text;
        }

        /// <summary>
        /// Combines the with relative URL.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <param name="url">The URL.</param>
        /// <param name="emptyIfRelativeUrlIsSlashOnly">if set to <c>true</c> [empty if relative URL is slash only].</param>
        /// <returns></returns>
        public static string CombineWithRelativeUrl(string relativeUrl, string url, bool emptyIfRelativeUrlIsSlashOnly = true)
        {
            if (emptyIfRelativeUrlIsSlashOnly)
            {
                relativeUrl = relativeUrl.Is("/") ? string.Empty : relativeUrl;
            }

            return relativeUrl + url;
        }

        /// <summary>
        /// Gets the server relative URL.
        /// </summary>
        /// <param name="absoluteUrl">The absolute URL.</param>
        /// <returns></returns>
        public static string GetServerRelativeUrl(string absoluteUrl)
        {
            if (string.IsNullOrEmpty(absoluteUrl))
                return absoluteUrl;

            var urlParts = absoluteUrl.Split('/');
            return string.Join("/", urlParts.Skip(3).ToArray());
        }

        /// <summary>
        /// Compares the version.
        /// </summary>
        /// <param name="version1">The version1.</param>
        /// <param name="version2">The version2.</param>
        /// <returns></returns>
        public static int CompareVersion(string version1, string version2)
        {
            const char VersionSeparator = '.';
            if (version1 != version2)
            {
                var versionParts1 = version1.Split(VersionSeparator);
                var versionParts2 = version2.Split(VersionSeparator);
                var minLength = versionParts1.Length;
                if (minLength > versionParts2.Length)
                    minLength = versionParts2.Length;
                for (var i = 0; i < minLength; i++)
                {
                    var number1 = int.Parse(versionParts1[i]);
                    var number2 = int.Parse(versionParts2[i]);
                    if (number1 > number2)
                        return 1;
                    else if (number1 < number2)
                        return -1;
                }
                if (minLength == versionParts1.Length)
                    return -1;
                else
                    return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// Prints the loaded assemblies to the Console.
        /// </summary>
        public static void PrintLoadedAssemblies()
        {
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine(AppDomain.CurrentDomain.Id);
            Console.WriteLine("***********************************************************************************");
            foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(ass.FullName);
            }
            Console.WriteLine("***********************************************************************************");
        }

        /// <summary>
        /// Replaces tags and content within with string.Empty
        /// </summary>
        /// <param name="text"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string ReplaceExcludeTags(string text, string begin, string end)
        {
            return Regex.Replace(text, string.Format(@"{0}(.+?){1}", begin, end), string.Empty, RegexOptions.Singleline);
        }

        /// <summary>
        /// Gets the time zone information.
        /// </summary>
        /// <param name="timeZone">The time zone.</param>
        /// <returns></returns>
        public static TimeZoneInfo GetTimeZoneInfo(string timeZone)
        {
            var timezoneInfo = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(tz => tz.DisplayName.StartsWith(timeZone));
            if (timezoneInfo == null)
            {
                var timezoneName = timeZone.Replace("and", "&");
                timezoneName = timezoneName.Substring(0, timezoneName.IndexOf(")") + 1);
                timezoneInfo = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(tz => tz.DisplayName.StartsWith(timezoneName));
            }

            return timezoneInfo;
        }

        /// <summary>
        /// Run defined actions in a retry scope when transient exceptions occurres
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="strategy">The strategy.</param>
        public static void TransientExceptionRetry(Action action, TransientExceptionRetryStrategy strategy)
        {
            int currentRetry = 0;
            for (; ; )
            {
                try
                {
                    action.Invoke();
                    break;
                }
                catch (Exception ex)
                {
                    currentRetry++;
                    // Check if the exception thrown was a transient exception
                    // based on the logic in the error detection strategy.
                    // Determine whether to retry the operation, as well as how 
                    // long to wait, based on the retry strategy.
                    if (currentRetry > strategy.RetryCount || !IsTransientException(ex, strategy))
                    {
                        // If this is not a transient error 
                        // or we should not retry re-throw the exception. 
                        throw;
                    }
                }

                // Calculate how long time to wait
                var waitTime = currentRetry == 1 ?
                    strategy.RetryDelayMilliseconds :
                    strategy.RetryDelayMilliseconds + (strategy.ExponentialDelayMilliseconds * (currentRetry - 1));
                System.Threading.Thread.Sleep(waitTime);
            }
        }

        private static bool IsTransientException(Exception ex, TransientExceptionRetryStrategy strategy)
        {
            //Check if the exception type is considered transient
            if (strategy.TransientExceptionTypes.Any(q => q == ex.GetType()))
                return true;

            //Check if the hresult code is considered transient
            if (strategy.TransientExceptionResultCodes.Any(q => q == ex.HResult))
                return true;

            //Check if the a custom matcher is considering this as a transient error
            if (strategy.TransientExceptionMatchers.Any(q => q.Invoke(ex)))
                return true;

            //Check if we should recursively check inner exceptions
            if (strategy.IncludeInnerExceptions && ex.InnerException.IsNotNull())
                return IsTransientException(ex.InnerException, strategy);

            return false;
        }


        public class TargetingDefinitionExceptionUtils
        {
            private const string TargetingDefinitionNotFound = "Cannot found targeting definition with ID {0}";

            public static string GenerateTargetingDefinitionNotFoundExceptionMsg(Guid id)
            {
                return string.Format(TargetingDefinitionExceptionUtils.TargetingDefinitionNotFound, id);
            }
        }
    }
}
