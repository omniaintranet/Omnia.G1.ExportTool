using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.G2
{
    public class Enums
    {
        /// <summary>
        /// Used for clientside type identification
        /// </summary>
        public enum NavigationNodeType
        {
            //The type used for external nodes i.e. not pointing to a wcm page
            Generic = 0,

            //A page in wcm, could be any of the supported types
            Page,
            PageType,
            PageCollection
        }

        public enum NavigationMigrationItemTypes
        {
            Link = 0,
            Page = 1
        }
    }
}
