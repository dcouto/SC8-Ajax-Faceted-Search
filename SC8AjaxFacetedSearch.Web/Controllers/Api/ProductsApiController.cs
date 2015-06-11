using SC8AjaxFacetedSearch.Models.Api;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SC8AjaxFacetedSearch.Models.Search.SearchTypes;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data;
using System.Linq.Expressions;
using System;

namespace SC8AjaxFacetedSearch.Controllers.Api
{
    public class ProductsApiController : ApiController
    {
        public GetFacetsAndResultsResponse GetFacetsAndResults()
        {
            using (var searcher = ContentSearchManager.GetIndex(string.Format("sitecore_{0}_index", Sitecore.Context.Database.Name)).CreateSearchContext())
            {
                var query = GetInitialQuery();

                // perform the search - returns facets and results
                // facet on the Category field
                var queryable = searcher.GetQueryable<ProductSearchResultItem>()
                    .Where(query)
                    .FacetOn(i => i.Category)
                    .FacetOn(i => i.AgeGroup);

                // since we faceted on one field only, we know we were returned exactly one "category" of facets
                var facetCategories = queryable
                    .GetFacets()
                    .Categories
                    .Select(i => new SC8AjaxFacetedSearch.Models.Api.FacetCategory(i))
                    .ToList();

                // convert our search results to Product objects
                var products = queryable
                    .GetResults()
                    .Hits
                    .Select(i => new Product(i.Document.GetItem()))
                    .ToList();

                return new GetFacetsAndResultsResponse(facetCategories, products);
            }
        }

        [HttpPost]
        public GetFacetsAndResultsResponse GetResultsOnly([FromBody] GetFacetsAndResultsRequest request)
        {
            if (request == null)
                return null;

            if (request.FacetCategories == null)
                return null;

            using (var searcher = ContentSearchManager.GetIndex(string.Format("sitecore_{0}_index", Sitecore.Context.Database.Name)).CreateSearchContext())
            {
                var query = GetInitialQuery();

                // add facets query if any facets are active
                if (request.FacetCategories.SelectMany(i => i.Facets).Any(i => i.Active))
                {
                    var facetsQuery = PredicateBuilder.False<ProductSearchResultItem>();

                    foreach (var facetCategory in request.FacetCategories)
                    {
                        switch (facetCategory.Name)
                        {
                            case "age_group":
                                foreach (var facet in facetCategory.Facets)
                                {
                                    if (facet.Active)
                                        facetsQuery = facetsQuery.Or(i => i.AgeGroup == ID.Parse(facet.Id));
                                }
                                break;

                            case "category":
                                foreach (var facet in facetCategory.Facets)
                                {
                                    if (facet.Active)
                                        facetsQuery = facetsQuery.Or(i => i.Category == ID.Parse(facet.Id));
                                }
                                break;
                        }
                    }

                    query = query.And(facetsQuery);
                }

                // add facets query if any facets are active
                //if (request.FacetCategories.Any(i => i.Active))
                //{
                //    var facetsQuery = PredicateBuilder.False<ProductSearchResultItem>();

                //    foreach (var facet in request.Facets)
                //    {
                //        if (facet.Active)
                //            facetsQuery = facetsQuery.Or(i => i.Category == ID.Parse(facet.Id));
                //    }

                //    query = query.And(facetsQuery);
                //}

                // perform the search - returns facets and results
                // facet on the Category field
                var queryable = searcher.GetQueryable<ProductSearchResultItem>()
                    .Where(query);

                // convert our search results to Product objects
                var products = queryable
                    .GetResults()
                    .Hits
                    .Select(i => new Product(i.Document.GetItem()))
                    .ToList();

                return new GetFacetsAndResultsResponse(null, products);
            }
        }

        private Expression<Func<ProductSearchResultItem, bool>> GetInitialQuery()
        {
            var query = PredicateBuilder.True<ProductSearchResultItem>();

            // results must be of template Product - /sitecore/templates/SC8 Ajax Faceted Search/Product
            query = query.And(i => i.TemplateId == ID.Parse("{7DB4066B-3401-48E5-B663-003EB167FA49}"));

            // results must be descendents of the Products item - /sitecore/content/Home/Products
            query = query.And(i => i.Paths.Contains(ID.Parse("{A930D7ED-0B19-44EA-A756-4A7EF71A50D6}")));

            return query;
        }
    }
}
