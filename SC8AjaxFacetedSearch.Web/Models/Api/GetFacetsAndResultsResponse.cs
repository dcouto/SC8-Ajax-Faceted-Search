using System.Collections.Generic;

namespace SC8AjaxFacetedSearch.Models.Api
{
    public class GetFacetsAndResultsResponse : GetFacetsAndResultsRequest
    {
        public IEnumerable<Product> Products { get; set; }

        public GetFacetsAndResultsResponse()
        {
            
        }

        public GetFacetsAndResultsResponse(IEnumerable<FacetCategory> facetCategories, IEnumerable<Product> products)
        {
            FacetCategories = facetCategories;
            Products = products;
        }
    }
}