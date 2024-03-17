using api.Dto;
using api.Models;

namespace api.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<Result> signUp (Register data);
        Task<Result> login (Login data);
        Task<Result> updateProfile (Register data);
        Task<Result> sendOTP (authPhone data);
        Task<Result> activeOTP (otpPhone data);

    }
}