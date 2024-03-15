using api.Dto;
using api.Models;

namespace api.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<Result> signUp (Register data);
        Task<Result> login (Login data);
    }
}