using api.Dto;
using api.Models;

namespace api.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<Result> signUp (Register data);
        Task<Result> login (Login data);
        Task<Result> loginPhone (LoginPhone data);
        Task<Result> updateProfile (Register data);
    }
    public interface IOtpRepository
    {
        Task<Result> sendOTP (SendOTP data, string? action);
        Task<Result> activeOTP (ActiveOTP data);
    }
}