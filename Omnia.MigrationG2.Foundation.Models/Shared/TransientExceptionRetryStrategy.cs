using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Models.Shared
{
    public class TransientExceptionRetryStrategy
    {
        /// <summary>
        /// Gets or sets the retry count default is 3.
        /// </summary>
        /// <value>
        /// The retry count.
        /// </value>
        public int RetryCount { get; set; }

        /// <summary>
        /// Gets or sets the retry delay default is 1000ms.
        /// </summary>
        /// <value>
        /// The retry delay.
        /// </value>
        public int RetryDelayMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets the exponential delay default is 0.
        /// </summary>
        /// <value>
        /// The exponential delay.
        /// </value>
        public int ExponentialDelayMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the retry operation should be done if a match is in a inner exception [include inner exceptions].
        /// </summary>
        /// <value>
        /// <c>true</c> if [include inner exceptions]; otherwise, <c>false</c>.
        /// </value>
        public bool IncludeInnerExceptions { get; set; }

        /// <summary>
        /// Gets or sets the exception types that should be treated as a transient exception.
        /// </summary>
        /// <value>
        /// The transient exceptions.
        /// </value>
        public List<Type> TransientExceptionTypes { get; set; }

        /// <summary>
        /// Gets or sets the transient exception HResult codes that should be treated as a transient exception.
        /// </summary>
        /// <value>
        /// The transient exception result codes.
        /// </value>
        public List<int> TransientExceptionResultCodes { get; set; }

        /// <summary>
        /// Gets or sets the transient exception matchers which can be used to create custom code to decide if the exception is transient.
        /// </summary>
        /// <value>
        /// The transient exception matchers.
        /// </value>
        public List<Func<Exception, bool>> TransientExceptionMatchers { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransientExceptionRetryStrategy"/> class.
        /// </summary>
        public TransientExceptionRetryStrategy()
        {
            RetryCount = 3;
            RetryDelayMilliseconds = 1000;
            ExponentialDelayMilliseconds = 0;
            TransientExceptionTypes = new List<Type>();
            TransientExceptionResultCodes = new List<int>();
        }

    }
}
