using Omnia.MigrationG2.Foundation.Core.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Core.Fields
{
    [DateTimeField(id: "F073E238-B5D8-4EE0-A850-4D418786FFC4",
            internalName: "OMIPublishingEndDate",
            Title = "$Localize:OMI.Core.Fields.OmniaPublishingEndDate.Name;",
            Description = "$Localize:OMI.Core.Fields.OmniaPublishingEndDate.Description;",
            Indexed = true,
            Group = Foundation.Core.Constants.Common.SiteColumnGroup)]
    public class OmniaPublishingEndDate : FieldBase
    {
    }
}
