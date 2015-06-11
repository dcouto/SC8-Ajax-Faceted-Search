using System.Collections.Generic;

namespace SC8AjaxFacetedSearch.Models.Api
{
    public class GetFacetsAndResultsRequest
    {
        public IEnumerable<FacetCategory> FacetCategories { get; set; }

        public GetFacetsAndResultsRequest()
        {
            
        }
    }
}