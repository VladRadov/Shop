using Microsoft.AspNetCore.Mvc;

using Shop.Interfaces;

namespace Shop.Controllers
{
    public class ProductsController : Controller
    {
        private IStorageProducts _storageProducts;

        public ProductsController(IStorageProducts storageProducts)
        {
            _storageProducts = storageProducts;
        }

        public ViewResult ListProducts()
        {
            var products = _storageProducts.GetProducts;
            return View(products);
        }
    }
}
