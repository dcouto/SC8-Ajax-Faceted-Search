using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace SC8AjaxFacetedSearch.Models.Api
{
    public class Product
    {
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }

        public Product()
        {
            
        }

        public Product(Item item)
        {
            ProductName = FieldRenderer.Render(item, "Product Name");
            Price = FieldRenderer.Render(item, "Price");

            var category = Sitecore.Context.Database.GetItem(ID.Parse(item["Category"]));

            if (category != null)
                Category = FieldRenderer.Render(category, "Category Name");
            else
                Category = item["Category"];
        }
    }
}