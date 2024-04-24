using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using api.Data;
using api.Dto;
using api.Models;
using api.Repository.Interface;

namespace api.Repository
{
    public class CarsMonumentRepository : IMonumentRepository
    {

        private readonly AppDbContext _AppDbContext;
        private readonly IBangkhenRepository _IBangkhenRepository;
        private readonly IComponentsRepository _IComponenetsRepository;


        public CarsMonumentRepository(AppDbContext AppDbContext, IBangkhenRepository IBangkhenRepository, IComponentsRepository IComponenetsRepository)
        {
            _AppDbContext = AppDbContext;
            _IBangkhenRepository = IBangkhenRepository;
            _IComponenetsRepository = IComponenetsRepository;
        }


        public async Task<Result> saveData(Cars data, string? action, IFormFile imageFile)
        {
            try
            {
                var res = _AppDbContext.Monument.Where(x => x.id == data.id).Select(x => x.roadImage).FirstOrDefault();

                string? validate = await _IBangkhenRepository.ValidateData(data);
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
                        imagePath = await _IBangkhenRepository.UploadImage(imageFile);
                    }

                    MonumentModel m = new();
                    if (action == "UPDATE")
                    {

                        var idRes = data.id;

                        if (idRes != null) 
                        {
                            m.id = data.id!;
                        }
                        else
                        {
                            return new Result
                            {
                                success = false,
                                result = "ไม่พบข้อมูล"
                            };
                        }
                    }
                    m.number = data.number;
                    m.firstStation = data.firstStation;
                    m.lastStation = data.lastStation;
                    m.roadDesc = data.roadDesc;
                    m.timeOut = data.timeOut;
                    m.roadImage = action == "UPDATE" && imageFile == null ? res : imagePath;

                    if (action == "CREATE")
                    {
                        await _AppDbContext.Monument.AddRangeAsync(m);
                    }
                    else if (action == "UPDATE")
                    {
                        _AppDbContext.Monument.UpdateRange(m);
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
                        result = "แก้ไขข้อมูลสำเร็จ"
                    };

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error on saveData : " + ex.Message);
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

        public async Task<Result> deleteData(Cars data, string? action)
        {
            var res = await _IComponenetsRepository.ComponentDelete(data, action);
            if (res != null)
            {
                return new Result
                {
                    success = true,
                    result = "ลบข้อมูลสำเร็จ"
                };
            }
            else
            {
                return new Result
                {
                    success = false,
                    result = "ไม่พบข้อมูล"
                };
            }
        }

        public async Task<Result> getMainCars(string? action)
        {
            var res = await _IComponenetsRepository.ComponentGetData(action);
            if (res != null)
            {
                return res;

            }
            else
            {
                return new Result
                {
                    success = false,
                    result = "ไม่พบข้อมูล"
                };
            }

        }

        public async Task<Result> getMainByID(int? id, string? action)
        {
            var res = await _IComponenetsRepository.ComponentGetByID(id, action);
            if (res != null)
            {
                return res;
            }
            else
            {
                return new Result
                {
                    success = false,
                    result = "ไม่พบข้อมูล"
                };
            }
        }
    }
}