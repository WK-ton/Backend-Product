using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
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

                // if (action == "CREATE")
                // {
                var checkDateTime = await _AppdbContext.BangKhen.FirstOrDefaultAsync(x => DateTime.Equals(x.timeOut, data.timeOut));
                if (checkDateTime != null)
                {
                    return new Result
                    {
                        success = false,
                        result = "วันที่และเวลาถูกบันทึกไปแล้ว"
                    };
                }
                // }
                using (TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled))
                {

                    string imagePath = null!;

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
    }



}