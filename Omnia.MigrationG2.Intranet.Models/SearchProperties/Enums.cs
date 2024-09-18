using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.SearchProperties
{
    public enum SearchPropertyFormatting : byte
    {
        NoFormat = 1,
        Date = 2,
    }

    public enum SearchPropertyCategory : byte
    {
        People = 1,
        Document = 2
    }
}
