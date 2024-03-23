using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.IO;
using System.Transactions;
using api.Data;
using api.Dto;
using api.Models;
using api.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace api.Repository
{
    public class CarsBangkhenRepository : IBangkhenRepository
    {
        private readonly AppDbContext _AppdbContext;

        public CarsBangkhenRepository(AppDbContext AppdbContext)
        {
            _AppdbContext = AppdbContext;
        }

        public async Task<Result> saveData(Cars data, string? action, IFormFile imageFile)
        {
            try
            {
                string? validate = await ValidateData(data);
                if (validate != null)
                {
                    return new Result
                    {
                        success = false,
                        result = validate
                    };
                }

                // var checkDateTime = await _AppdbContext.BangKhen.FirstOrDefaultAsync(x => DateTime.Equals(x.timeOut, data.timeOut));
                // if (checkDateTime != null)
                // {
                //     return new Result
                //     {
                //         success = false,
                //         result = "วันที่และเวลาถูกบันทึกไปแล้ว"
                //     };
                // }

                using (TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled))
                {

                    string? imagePath = null!;

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        imagePath = await UploadImage(imageFile);
                    }

                    BangKhenModel BK = new();
                    BK.id = data.id!;
                    BK.number = data.number;
                    BK.firstStation = data.firstStation;
                    BK.lastStation = data.lastStation;
                    BK.roadDesc = data.roadDesc;
                    BK.timeOut = data.timeOut;
                    BK.roadImage = imagePath;


                    if (action == "CREATE")
                    {
                        await _AppdbContext.BangKhen.AddRangeAsync(BK);
                    }
                    else if (action == "UPDATE")
                    {
                        _AppdbContext.BangKhen.Update(BK);
                    }

                    await _AppdbContext.SaveChangesAsync();

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
                        result = "แก้ไขข้อมูลสําเร็จ"
                    };

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error on CreateData : " + ex.Message);
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

        public async Task<string?> ValidateData(Cars data)
        {
            if (String.IsNullOrEmpty(data.firstStation)) return "กรุณากรอกชื่อสถานีต้นทาง";
            if (String.IsNullOrEmpty(data.lastStation)) return "กรุณากรอกชื่อสถานีปลายทาง";
            if (String.IsNullOrEmpty(data.number)) return "กรุณากรอกเลขรถประจำทาง";
            if (data.timeOut == null || data.timeOut == DateTime.MinValue) return "กรุณากรอกเวลารถออก";

            return null;

        }

        public async Task<string?> UploadImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return "ไม่มีไฟล์อัพโหลด";
                }

                string fileName = "_" + Guid.NewGuid().ToString() + Path.GetFileName(file.FileName); string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                string filePath = Path.Combine(uploadFolder, fileName);

                Directory.CreateDirectory(uploadFolder);

                using (FileStream stream = new(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Path.Combine("images", fileName);

            }
            catch (Exception ex)
            {
                throw new Exception("Error on UploadImage : " + ex.Message);
            }
        }

        public async Task<Result> DeleteHub(Cars data, string? action)
        {
            try
            {
                switch (action)
                {
                    case "Bangkhen":
                        var BangKhenDelete = await _AppdbContext.BangKhen.FirstOrDefaultAsync(x => x.id == data.id);
                        if (BangKhenDelete != null)
                        {
                            _AppdbContext.BangKhen.Remove(BangKhenDelete);


                            await _AppdbContext.SaveChangesAsync();

                            return new Result
                            {
                                success = true,
                                result = "ลบข้อมูลสำเร็จ"
                            };
                        }
                        break;

                    case "Monument":


                        var MonumentDelete = await _AppdbContext.Monument.FirstOrDefaultAsync(x => x.id == data.id);
                        if (MonumentDelete != null)
                        {
                            _AppdbContext.Monument.Remove(MonumentDelete);


                            await _AppdbContext.SaveChangesAsync();

                            return new Result
                            {
                                success = true,
                                result = "ลบข้อมูลสำเร็จ"
                            };
                        }

                        break;

                    case "Morchit":

                        var MorchitDelete = await _AppdbContext.Morchit.FirstOrDefaultAsync(x => x.id == data.id);
                        if (MorchitDelete != null)
                        {
                            _AppdbContext.Morchit.Remove(MorchitDelete);


                            await _AppdbContext.SaveChangesAsync();

                            return new Result
                            {
                                success = true,
                                result = "ลบข้อมูลสำเร็จ"
                            };
                        }
                        break;
                }

                return new Result
                {
                    success = false,
                    result = "ไม่สามารถลบข้อมูลสำเร็จ"
                };

            }
            catch (Exception ex)
            {
                throw new Exception("Error on DeleteData" + ex.Message);
            }
        }

        public Task<Result> deleteData(Cars data)
        {
            return DeleteHub(data, "Bangkhen");
        }
    }



}