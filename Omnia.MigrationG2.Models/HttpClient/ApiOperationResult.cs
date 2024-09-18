using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Models.HttpClient
{
    public class ApiOperationResult<T>
    {
        public T Data { get; set; }
    }
}
