using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.ContentManagement
{
    public class LanguageLocale
    {
        public int LCID { get; set; }
        public string LanguageName { get; set; }
        public string Locale { get; set; }
        public string FlagName { get; set; }
        public bool IsSourceLang { get; set; }
    }
}
