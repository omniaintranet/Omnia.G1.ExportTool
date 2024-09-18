using Omnia.MigrationG2.Foundation.Core.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Core.Fields
{
    [DateTimeField(id: "84ED0452-1108-4133-BBD4-7B0CF2C4AABD",
        internalName: "OMIReviewDate",
        Title = "$Localize:OMI.Core.Fields.OmniaReviewDate.Name;",
        Description = "$Localize:OMI.Core.Fields.OmniaReviewDate.Description;",
        Indexed = true,
        Group = Foundation.Core.Constants.Common.SiteColumnGroup)]
    public class OmniaReviewDateField : FieldBase
    {

    }
}
