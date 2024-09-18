using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.ContentTypes.BuiltIn
{    
    /// <summary>
    /// The system page layout.
    /// </summary>
    [ContentTypeRef(id: "0x01010007FF3E057FA8AB4AA42FCB67B453FFC1")]
    public class SystemPageLayout : Document
    {
    }

    /// <summary>
    /// The page layout.
    /// </summary>
    [ContentTypeRef(id: "0x01010007FF3E057FA8AB4AA42FCB67B453FFC100E214EEE741181F4E9F7ACC43278EE811")]
    public class PageLayout : SystemPageLayout
    {
    }

    /// <summary>
    /// The display template.
    /// </summary>
    [ContentTypeRef(id: "0x0101002039C03B61C64EC4A04F5361F3851066")]
    public class DisplayTemplate : Document
    {
    }

    /// <summary>
    /// The item display template.
    /// </summary>
    [ContentTypeRef(id: "0x0101002039C03B61C64EC4A04F5361F385106603")]
    public class ItemDisplayTemplate : DisplayTemplate
    {
    }

    /// <summary>
    /// The system page.
    /// </summary>
    [ContentTypeRef(id: "0x010100C568DB52D9D0A14D9B2FDCC96666E9F2")]
    public class SystemPage : Document
    {
    }

    /// <summary>
    /// The page.
    /// </summary>
    [ContentTypeRef(id: "0x010100C568DB52D9D0A14D9B2FDCC96666E9F2007948130EC3DB064584E219954237AF39")]
    public class Page : SystemPage
    {
    }

    /// <summary>
    /// The article page.
    /// </summary>
    [ContentTypeRef(id: "0x010100C568DB52D9D0A14D9B2FDCC96666E9F2007948130EC3DB064584E219954237AF3900242457EFB8B24247815D688C526CD44D",
        Name = "$Localize:OMF.Common.ContentType.ArticlePage.Name;")]
    public class ArticlePage : Page
    {
    }

    /// <summary>
    /// The master page.
    /// </summary>
    [ContentTypeRef(id: "0x010105")]
    public class MasterPage : Document
    {
    }

    /// <summary>
    /// The folder.
    /// </summary>
    [ContentTypeRef(id: "0x0120")]
    public class Folder : Item
    {
    }
}
