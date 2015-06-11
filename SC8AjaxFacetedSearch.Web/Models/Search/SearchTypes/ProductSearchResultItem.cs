using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;

namespace SC8AjaxFacetedSearch.Models.Search.SearchTypes
{
    public class ProductSearchResultItem : SearchResultItem
    {
        [IndexField("product_name")]
        public string ProductName { get; set; }

        [IndexField("price")]
        public string Price { get; set; }

        [IndexField("category")]
        public ID Category { get; set; }

        [IndexField("age_group")]
        public ID AgeGroup { get; set; }
    }
}