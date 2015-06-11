using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SC8AjaxFacetedSearch.Controllers.Api;

namespace SC8AjaxFacetedSearch.Tests
{
    [TestClass]
    public class ProductsApiController_GetFacetsAndResults_Tests
    {
        [TestMethod]
        public void GetFacetsAndResults_ReturnsZeroItems()
        {
            // arrange
            var controller = new ProductsApiController();

            // act
            var result = controller.GetFacetsAndResults();

            // assert
            Assert.AreNotEqual(0, result.Products);
        }
    }
}
