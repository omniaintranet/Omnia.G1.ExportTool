using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.News
{
    public enum QueryFilterOperator
    {
        Equal = 1,
        NotEqual = 2,
        GreaterThan = 3,
        GreaterThanOrEqual = 4,
        LessThan = 5,
        LessThanOrEqual = 6,
        Contains = 7,
        StartsWith = 8,
        IsNull = 9
    }
}
