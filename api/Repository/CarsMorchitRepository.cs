using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using api.Data;
using api.Dto;
using api.Models;
using api.Repository.Interface;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace api.Repository
{
    public class CarsMorchitRepository : IMorchitRepository
    {
        private readonly AppDbContext _AppDbContext;
        private readonly IBangkhenRepository _ICarsBangkhenRepository;

        public CarsMorchitRepository(AppDbContext AppdbContext, IBangkhenRepository IBangkhenRepository)
        {
            _AppDbContext = AppdbContext;
            _ICarsBangkhenRepository = IBangkhenRepository;
        }

        public async Task<Result> saveData(Cars data, string? action, IFormFile imageFile)
        {
            try
            {
                string? validate = await _ICarsBangkhenRepository.ValidateData(data);
                if (validate != null)
                {
                    return new Result
                    {
                        success = false,
                        result = validate

                    };

                }

                using (TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled))
                {
                    string? imagePath = null;

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        imagePath = await _ICarsBangkhenRepository.UploadImage(imageFile);
                    }

                    MorchitModel m = new();
                    m.id = data.id;
                    m.number = data.number;
                    m.firstStation = data.firstStation;
                    m.lastStation = data.lastStation;
                    m.roadDesc = data.roadDesc;
                    m.timeOut = data.timeOut;
                    m.roadImage = imagePath;

                    if (action == "CREATE")
                    {
                        await _AppDbContext.Morchit.AddRangeAsync(m);
                    }
                    else if (action == "UPDATE")
                    {
                        _AppDbContext.Morchit.UpdateRange(m);
                    }

                    await _AppDbContext.SaveChangesAsync();

                    scope.Complete();

                }

                if (action == "CREATE")
                {
                    return new Result
                    {
                        success = true,
                        result = "บันทึกข้อมูลสําเร็จ"

                    };
                }
                else
                {
                    return new Result
                    {
                        success = true,
                        result = "แก้ไขข้อมูลสําเร็จ",
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error on saveData" + ex.Message);

            }
        }

        public Task<Result> createData(Cars data)
        {
            return saveData(data, "CREATE", data.roadImage!);
        }

        public Task<Result> updateData(Cars data)
        {
            return saveData(data, "UPDATE", data.roadImage!);
        }
    }
}