using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Fields
{
    /// <summary>
    /// ListItemMenuState
    /// </summary>
    public enum ListItemMenuState
    {
        Allowed = 0,
        Required = 1,
        Prohibited = 2
    }

    /// <summary>
    /// RichTextMode
    /// </summary>
    public enum RichTextMode
    {
        Compatible = 0,
        FullHtml = 1,
        HtmlAsXml = 2,
        ThemeHtml = 3
    }
}
