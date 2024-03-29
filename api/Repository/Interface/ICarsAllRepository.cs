using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;

namespace api.Repository.Interface
{
    public interface IBangkhenRepository
    {
        Task<Result> createData(Cars data);
        Task<Result> updateData(Cars data);
        Task<string?> ValidateData(Cars data);
        Task<string?> UploadImage(IFormFile file);
        Task<Result> deleteData(Cars data, string? action);
        Task<Result> getMainCars(string? action);
        Task<Result> getMainByID(int? id, string? action);
    }

    public interface IMorchitRepository
    {
        Task<Result> createData(Cars data);
        Task<Result> updateData(Cars data);
        Task<Result> deleteData(Cars data, string? action);
        Task<Result> getMainCars(string? action);
        Task<Result> getMainByID(int? id, string? action);



    }
    public interface IMonumentRepository
    {
        Task<Result> createData(Cars data);
        Task<Result> updateData(Cars data);
        Task<Result> deleteData(Cars data, string? action);
        Task<Result> getMainCars(string? action);
        Task<Result> getMainByID(int? id, string? action);
    }

    public interface IComponentsRepository
    {

        Task<Result> ComponentDelete(Cars data, string? action);
        Task<Result> ComponentGetData(string? action);
        Task<Result> ComponentGetByID(int? id, string? action);

    }
}