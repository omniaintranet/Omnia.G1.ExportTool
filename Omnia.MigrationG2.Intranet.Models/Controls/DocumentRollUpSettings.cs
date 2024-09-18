using System;
using System.Collections.Generic;

namespace Omnia.MigrationG2.Intranet.Models.Controls
{
    public class DocumentRollUpSettings
    {
        public int sortByDirection { get; set; }
        public int refinerLocation { get; set; }
        public int pageSize { get; set; }
        public bool showSearchBox { get; set; }
        public bool isOpenInOffice { get; set; }
        public bool isOpenLinkInNewWindow { get; set; }
        public string queryText { get; set; }
        public bool isInitValue { get; set; }
        public TitleSettings titleSettings { get; set; }
        public string textColor { get; set; }
        public string bgColor { get; set; }
        public string borderColor { get; set; }
        public SortByProperty sortByProperty { get; set; }
        public List<SortByProperty> columns { get; set; }
        public List<SortByProperty> refiners { get; set; }
    }
    public class SortByProperty
    {
        public Guid id { get; set; }
        public string displayName { get; set; }
        public string managedProperty { get; set; }
        public string managedRetrieveProperty { get; set; }
        public string managedRefinerProperty { get; set; }
        public string managedQueryProperty { get; set; }
        public string managedSortProperty { get; set; }
        public bool isTitleProperty { get; set; }
        public string tenantId { get; set; }
        public bool retrievable { get; set; }
        public bool refinable { get; set; }
        public bool queryable { get; set; }
        public bool sortable { get; set; }
        public int formatting { get; set; }
        public int category { get; set; }
        public string displayNameInText { get; set; }
        public int refinerLimit { get; set; }
        public int refinerOrderBy { get; set; }
        public string widthType { get; set; }
        public bool isShowColumn { get; set; }
        public bool isShowRefiner { get; set; }
    }
}
