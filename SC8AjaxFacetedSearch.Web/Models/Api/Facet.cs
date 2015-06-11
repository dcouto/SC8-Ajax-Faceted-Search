using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;
using Sitecore.Web.UI.WebControls;

namespace SC8AjaxFacetedSearch.Models.Api
{
    public class Facet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public bool Active { get; set; }

        public Facet()
        {
            
        }

        public Facet(string id, int count)
        {
            Id = id;

            var item = Sitecore.Context.Database.GetItem(ID.Parse(id));

            if (item != null)
                Name = FieldRenderer.Render(item, "Display Text");
            else
                Name = id;
            
            Count = count;

            Active = false;
        }
    }
}