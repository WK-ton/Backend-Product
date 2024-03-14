using api.Dto;
using api.Models;

namespace api.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<Result> GetAllProducts();
        Task<Result> AddProduct(Product product);
        Task<Result> signUp (Register data);
        
    }
}