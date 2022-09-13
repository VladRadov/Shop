using Shop.Models;

namespace Shop.Interfaces
{
    public interface IStorageProducts
    {
        IEnumerable<Product> GetProducts { get; }
    }
}
