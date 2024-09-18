using System;
using System.Collections.Generic;

namespace Omnia.MigrationG2.Intranet.Models.Controls
{
    public class PeopleRollUpSettings
    {
        public string queryType { get; set; }
        public string title { get; set; }
        public int pageSize { get; set; }
        public bool showProfileImage { get; set; }
        public object queryData { get; set; }
        public string pagingType { get; set; }
        public int pageLinkSize { get; set; }
        public string sortByProperty { get; set; }
        public int sortByDirection { get; set; }
        public int totalColumns { get; set; }
        public string refinerLocation { get; set; }
        public bool showSearchBox { get; set; }
        public int refinerLayout { get; set; }
        public List<SelectedProperties> selectedProperties { get; set; }
        public TitleSettings titleSettings { get; set; }
        public List<Refiners> refiners { get; set; }
    }
    public class SelectedProperties
    {
        public Guid id { get; set; }
        public bool showLabel { get; set; }
        public string themeColor { get; set; }
    }
    public class Refiners
    {
        public Guid id { get; set; }
        public int refinerLimit { get; set; }
        public int refinerOrderBy { get; set; }
    }
}
