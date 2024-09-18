using System;

namespace Omnia.MigrationG2.Intranet.Models.Controls
{
    public class BannerSettings
    {
        public string title { get; set; }
        public TitleSettings titleSettings { get; set; }
        public string customTitle { get; set; }
        public string imageUrl { get; set; }
        public string content { get; set; }
        public string linkUrl { get; set; }
        public string footer { get; set; }
        public string titleColor { get; set; }
        public string contentColor { get; set; }
        public string footerColor { get; set; }
        public string backgroundColor { get; set; }
        public string viewId { get; set; }
        public string bannerType { get; set; }
        public bool isOpenLinkNewWindow { get; set; }
        public TargetingSettings targeting { get; set; }
        public float? opacity { get; set; } 
        public string templateName { get; set; }
        public string groupName { get; set; }
    }
}
