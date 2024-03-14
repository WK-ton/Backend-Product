using api.Dto;
using api.Models;

namespace api.Repository.Interface
{
    public interface IProductRepository
    {
        Task<Result> GetAllProducts();
        Task<Result> AddProduct(Product product);
        
    }
}