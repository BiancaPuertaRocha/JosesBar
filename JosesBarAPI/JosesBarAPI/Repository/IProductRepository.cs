using JosesBarAPI.Entities;
using JosesBarAPI.Dtos;

namespace JosesBarAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product?> GetProductByID(int productId);
        Task<Product> InsertProduct(CreateProduct product);
        Task<bool> DeleteProduct(int productID);
        Task<Product?> UpdateProduct(UpdateProduct product, int id);
        Task<List<Product>> GetProductByDescription(string descriprion);
    }
}
