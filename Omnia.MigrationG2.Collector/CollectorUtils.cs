using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector
{
    public static class CollectorUtils
    {
        private static readonly string LAYOUT_ID = "layoutId";
        private static readonly string CONTROLS = "controls";
        private static readonly string IS_STATIC = "isStatic";

        public static Guid GetGlueLayoutId(string rawGlueData)
        {
            var glueData = JsonConvert.DeserializeObject<JObject>(rawGlueData);
            if (glueData[LAYOUT_ID].HasValues)
            {
                return new Guid(glueData[LAYOUT_ID].Value<string>());
            }

            return Guid.Empty;
        }

        public static bool HasNonStaticBlock(string rawGlueData)
        {
            return false;
        }
    }
}
