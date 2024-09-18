using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Core
{
    public static class Constants
    {
        public const string AppSettingsFile = "appsettings.json";

        public static class AppSettings
        {
            public const string OmniaSettingsSection = "OmniaSettings";

            public const string ConnectionStringsSection = "ConnectionStrings";

            public const string FoundationConnectionString = "Foundation";

            public const string IntranetConnectionString = "Intranet";

            public const string MigrationSettingsSection = "MigrationSettings";

            public const string OutputSettingsSection = "OutputSettings";

            public const string UserSettingsSection = "UserCredential";

            public const string HttpClientSettingsSection = "HttpClientSettings";
        }

        public static class DefaultProperties
        {
            public const string PageContent = "PageContent";

            public const string PageSummary = "PageSummary";

            public const string PageImage = "PageImage";

            public const string PublishingContact = "PublishingContact";

            public const string FileMajorVersionCreatedAt = "FileMajorVersionCreatedAt";

            public const string CreatedBy = "CreatedBy";

            public const string CreatedAt = "CreatedAt";

            public const string ModifiedBy = "ModifiedBy";

            public const string ModifiedAt = "ModifiedAt";
        }

        public class G1GlueLayoutIDs
        {
            public static readonly Guid PageWithLeftNav = new Guid("b8cc18d8-1689-4ead-a934-f994619bf8be");
            public static readonly Guid PageWithoutLeftNav = new Guid("3298d377-0930-4bd1-9731-32585a0a0f65");
            public static readonly Guid StartPage = new Guid("be19d5ba-90bd-4cef-9d45-581362b75a75");
            public static readonly Guid NewsArticle = new Guid("6cf7e43b-80d6-4597-9080-8d8969c23311");
        }

        public class G1ControlIDs
        {
            public static readonly Guid Banner = new Guid("6F4F4334-1052-4AB4-A823-A3072951DB85");
            public const string BannerIdString = "6F4F4334-1052-4AB4-A823-A3072951DB85";

            public static readonly Guid DocumentRollup = new Guid("97AE49DF-2B1B-45C7-B3BE-873721313120");
            public const string DocumentRollupIdString = "97AE49DF-2B1B-45C7-B3BE-873721313120";

            public static readonly Guid PeopleRollup = new Guid("E03EEBBA-CA3D-46FF-ABFE-E75FFFAB5177");
            public const string PeopleRollupIdString = "E03EEBBA-CA3D-46FF-ABFE-E75FFFAB5177";

            public static readonly Guid NewsViewer = new Guid("FB58CF64-F53E-4A45-A489-DF747555834B");
            public const string NewsViewerIdString = "FB58CF64-F53E-4A45-A489-DF747555834B";

            // ODM
            public static readonly Guid ControlledDocumentView = new Guid("C07E8885-6B79-4A0A-A4DC-5522A78BB0F7");
            public const string ControlledDocumentViewIdString = "C07E8885-6B79-4A0A-A4DC-5522A78BB0F7";

            public static readonly Guid ScriptHtml = new Guid("82850DC5-971D-44DB-B058-BF8FD5D84FFD");
            public const string ScriptHtmlIdString = "82850DC5-971D-44DB-B058-BF8FD5D84FFD";

            public static readonly Guid Accordion = new Guid("1D0A2C83-87BC-4803-8B51-0574F9EA6CD4");
            public const string AccordionIdString = "1D0A2C83-87BC-4803-8B51-0574F9EA6CD4";

            //3df94231-87a0-40c5-812d-8ade73691f36 - SVG Viewer
        }
    }
}
