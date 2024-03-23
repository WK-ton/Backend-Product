using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;

namespace api.Repository.Interface
{
    public interface IBangkhenRepository
    {
        Task<Result> createData (Cars data);
        Task<Result> updateData (Cars data);
        Task<string?> ValidateData (Cars data);
        Task<string?> UploadImage (IFormFile file);  
        Task<Result> deleteData(Cars data);
        Task<Result> DeleteHub (Cars data, string? action);  
    }

    public interface IMorchitRepository
    {
        Task<Result> createData (Cars data);
        Task<Result> updateData (Cars data);
        Task<Result> deleteData (Cars data, string? action);
    }
    public interface IMonumentRepository
    {
        Task<Result> createData (Cars data);
        Task<Result> updateData (Cars data);
        Task<Result> deleteData (Cars data, string? action);
    }
}