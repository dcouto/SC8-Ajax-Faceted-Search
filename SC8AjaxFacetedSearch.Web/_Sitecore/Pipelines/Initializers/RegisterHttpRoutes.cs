using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Sitecore.Pipelines;

namespace SC8AjaxFacetedSearch._Sitecore.Pipelines.Initializers
{
    public class RegisterHttpRoutes
    {
        public void Process(PipelineArgs args)
        {
            GlobalConfiguration.Configure(Configure);
        }

        protected void Configure(HttpConfiguration configuration)
        {
            var routes = configuration.Routes;
            routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}");
        }
    }
}