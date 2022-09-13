using Shop.Services;
using Shop.Interfaces;

namespace Shop.Models.Data
{
    class StorageProducts : IStorageProducts
    {
        private BaseQuery _query;

        private List<Product> _products;

        public IEnumerable<Product> GetProducts
        {
            get
            {
                GetAllData();
                return _products;
            }
        }

        public void GetAllData()
        {
            _query = new SelectProdcuts();
            _products = new List<Product>();
            _query.Execute();

            for (int i = 0; i < _query.CountItemsOfTableResult(); i++)
            {
                _products.Add(
                    new Product()
                    {
                        Name = _query.GetSubitemsOfItem(i, "Название товара"),
                        Description = _query.GetSubitemsOfItem(i, "Описание товара"),
                        ProductNameCategory = _query.GetSubitemsOfItem(i, "Категория товара"),
                    });
            }
        }
    }
}
