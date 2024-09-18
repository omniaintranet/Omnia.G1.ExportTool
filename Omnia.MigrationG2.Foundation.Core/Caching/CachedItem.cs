using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Caching
{
    public class CachedItem
    {

        private string _region;

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public string Region
        {
            get
            {
                return _region;
            }

            set
            {
                _region = value.ToLower();
            }

        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        internal object Value { get; set; }

        /// <summary>
        /// Gets or sets the json.
        /// </summary>
        /// <value>
        /// The json.
        /// </value>
        internal string Json { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CachedItem"/> is encrypted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if encrypted; otherwise, <c>false</c>.
        /// </value>
        public bool Encrypted { get; set; }

        /// <summary>
        /// Gets or sets the expires at.
        /// </summary>
        /// <value>
        /// The expires at.
        /// </value>
        public DateTimeOffset? ExpiresAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedItem"/> class.
        /// </summary>
        public CachedItem() { }


        /// <summary>
        /// Initializes a new instance of the <see cref="CachedItem"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="content">The content.</param>
        /// <param name="expires">The expires.</param>
        public CachedItem(string key, object value, DateTimeOffset? expires, string region)
        {
            Key = key;
            Value = value;
            ExpiresAt = expires;
            Region = region;

            Validate();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedItem"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="encrypted">if set to <c>true</c> the object will be [encrypted].</param>
        /// <param name="expires">The expires.</param>
        public CachedItem(string key, object value, bool encrypted, DateTimeOffset? expires, string region)
        {
            Key = key;
            Value = value;
            ExpiresAt = expires;
            Encrypted = encrypted;
            Region = region;

            Validate();
        }


        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <exception cref="System.Exception">The Region property has a maximum length of 128 characters</exception>
        private void Validate()
        {
            if (Region.Length > 128)
            {
                throw new Exception("The Region property has a maximum length of 128 characters");
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            if (Json.IsNotNull())
            {
                Value = JsonConvert.DeserializeObject<T>(Json);
                Json = string.Empty;
            }

            return (T)Value;
        }

    }
}
