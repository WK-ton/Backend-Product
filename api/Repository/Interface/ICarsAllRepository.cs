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
    }

    public interface IMorchitRepository
    {
        Task<Result> createData (Cars data);
        Task<Result> updateData (Cars data);
    }
    public interface IMonumentRepository
    {
        Task<Result> createData (Cars data);
        Task<Result> updateData (Cars data);
    }
}