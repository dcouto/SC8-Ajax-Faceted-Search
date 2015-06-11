using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC8AjaxFacetedSearch.Models.Api
{
    public class FacetCategory
    {
        public string Name { get; set; }
        public IEnumerable<Facet> Facets { get; set; }

        public FacetCategory()
        {
            
        }
        
        public FacetCategory(Sitecore.ContentSearch.Linq.FacetCategory scFacetCategory)
        {
            Name = scFacetCategory.Name;
            Facets = scFacetCategory.Values.Select(i => new Facet(i.Name, i.AggregateCount));
        }
    }
}