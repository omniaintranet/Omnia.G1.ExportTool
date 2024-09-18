using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Models.Extensibility
{
    public class SPTokenExpiredException : Exception
    {
        public SPTokenExpiredException(string message)
        : base(message)
        {
        }
    }
}
