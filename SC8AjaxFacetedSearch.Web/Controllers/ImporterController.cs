using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SC8AjaxFacetedSearch.Models.Importer;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.SecurityModel;
using Sitecore.Data.Items;

namespace SC8AjaxFacetedSearch.Controllers
{
    public class ImporterController : Controller
    {
        // GET: Importer
        public ActionResult Index()
        {
            var viewModel = new Importer();

            using (var db = new AdventureWorksDataContext(ConfigurationManager.ConnectionStrings["adventureworks"].ConnectionString))
            {
                using (new SecurityDisabler())
                {
                    using (new BulkUpdateContext())
                    {
                        /*
                        var scCategories = new List<Item>();
                        
                        // Categories
                        var categoriesRootItem = Sitecore.Context.Database.GetItem(ID.Parse("{C3BB610C-9CF7-4663-96BA-DBB14B02FA15}"));

                        if (categoriesRootItem != null)
                        {
                            var metadataTemplateId = new TemplateID(ID.Parse("{17DA53E5-AA9E-442F-A719-2BD2BB35F113}"));

                            foreach (var cat in db.ProductCategories)
                            {
                                var newCat = categoriesRootItem.GetChildren().FirstOrDefault(i => i.Name == cat.Name);

                                if (newCat == null)
                                {
                                    newCat = categoriesRootItem.Add(cat.Name, metadataTemplateId);

                                    if (newCat != null)
                                    {
                                        newCat.Editing.BeginEdit();

                                        newCat["Legacy ID"] = cat.ProductCategoryID.ToString();

                                        newCat.Editing.EndEdit();
                                    }
                                }

                                scCategories.Add(newCat);
                            }
                        }
                        else
                            viewModel.Messages.Add("Categories root item not found.");
                        */

                        // Products
                        var productsRootItem = Sitecore.Context.Database.GetItem(ID.Parse("{A930D7ED-0B19-44EA-A756-4A7EF71A50D6}"));

                        if (productsRootItem != null)
                        {
                            var random = new Random();

                            var ageGroups = new List<Item>();
                            var ageGroupsRootItem =
                                Sitecore.Context.Database.GetItem(ID.Parse("{2EF5FA02-6864-4C18-A5ED-4C798C4E50F8}"));

                            if (ageGroupsRootItem != null)
                                ageGroups = ageGroupsRootItem.GetChildren().ToList();

                            var productsTemplateId = new TemplateID(ID.Parse("{7DB4066B-3401-48E5-B663-003EB167FA49}"));

                            foreach (var prod in db.Products)
                            {
                                var scValidName = ItemUtil.ProposeValidItemName(prod.Name);

                                var newProd = productsRootItem.GetChildren().FirstOrDefault(i => i.Name == scValidName);

                                if (newProd == null)
                                    newProd = productsRootItem.Add(scValidName, productsTemplateId);

                                if (newProd != null)
                                {
                                    // get matching Sitecore Category
                                    //var scMatchingCat = scCategories.FirstOrDefault(i => i["Legacy ID"] == prod.ProductCategoryID.Value.ToString());

                                    //if (scMatchingCat != null)
                                    //{
                                    newProd.Editing.BeginEdit();

                                    // age group
                                    var agItem = ageGroups[random.Next(0, 4)];

                                    if (agItem != null)
                                        newProd["Age Group"] = agItem.ID.ToString();

                                    //newProd["Price"] = string.Format("{0:N2}", prod.ListPrice);
                                    //newProd["Category"] = scMatchingCat.ID.ToString();

                                    newProd.Editing.EndEdit();
                                    //}
                                }
                            }
                        }
                        else
                            viewModel.Messages.Add("Products root item not found.");
                    }
                }
            }

            if(viewModel.Messages.Count == 0)
                viewModel.Messages.Add("Categories and Products imported succesfully.");

            return View(viewModel);
        }
    }
}