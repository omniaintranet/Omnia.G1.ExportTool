using Omnia.MigrationG2.Foundation.Core.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Core.Fields
{
    [DateTimeField(id: "D90FBA12-58C6-4070-8FD7-ED2E3AB5929D",
        internalName: "OMIPublishingStartDate",
        Title = "$Localize:OMI.Core.Fields.OmniaPublishingStartDate.Name;",
        Description = "$Localize:OMI.Core.Fields.OmniaPublishingStartDate.Description;",
        Indexed = true,
        Group = Foundation.Core.Constants.Common.SiteColumnGroup)]
    public class OmniaPublishingStartDate : FieldBase
    {

    }
}
