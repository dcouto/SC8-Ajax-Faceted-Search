function FacetCategoryModel(serverFacetCategory)
{
    var self = this;

    self.name = serverFacetCategory.Name;

    var newFacets = [];

    $.each(serverFacetCategory.Facets, function (index, value) {
        newFacets.push(new FacetModel(value));
    });

    self.facets = ko.observableArray(newFacets);
}

function FacetModel(serverFacet)
{
    var self = this;

    self.name = serverFacet.Name;
    self.count = serverFacet.Count;
    self.id = serverFacet.Id;

    self.active = ko.observable(false);
    self.active.subscribe(function (newValue) {
        var request = {
            facetCategories: []
        };

        $.each(viewModel.facetCategories(), function (index, currentCat) {
            request.facetCategories.push({
                name: currentCat.name,
                facets: currentCat.facets()
            });
        });

        $.post('/Api/ProductsApi/GetResultsOnly', request, function (response) {
            viewModel.products(response.Products);
        });
    });
}

function ProductsViewModel()
{
    var self = this;

    self.facetCategories = ko.observableArray();
    self.products = ko.observableArray();

    $.getJSON('/Api/ProductsApi/GetFacetsAndResults', function (response) {
        var newFacetCategories = [];

        $.each(response.FacetCategories, function(index, value) {
            newFacetCategories.push(new FacetCategoryModel(value));
        });

        self.facetCategories(newFacetCategories);

        self.products(response.Products);
    });
}

var viewModel = new ProductsViewModel();

$(function() {
    ko.applyBindings(viewModel);
});
