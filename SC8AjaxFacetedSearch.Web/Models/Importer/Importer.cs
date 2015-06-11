using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC8AjaxFacetedSearch.Models.Importer
{
    public class Importer
    {
        public List<string> Messages { get; set; }

        public Importer()
        {
            Messages = new List<string>();
        }
    }
}