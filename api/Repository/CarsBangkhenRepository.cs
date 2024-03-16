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
    public class CarsBangkhenRepository : ICarsBangkhenRepository
    {
        private readonly AppDbContext _AppdbContext;

        public CarsBangkhenRepository(AppDbContext AppdbContext)
        {
            _AppdbContext = AppdbContext;
        }

        public async Task<Result> CreateData(BangKhen data)
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

                var checkDateTime = await _AppdbContext.BangKhen.FirstOrDefaultAsync(x => DateTime.Equals(x.timeOut, data.timeOut));
                if(checkDateTime != null)
                {
                    return new Result
                    {
                        success = false,
                        result = "วันที่และเวลาถูกบันทึกไปแล้ว"
                    };
                }

                using (TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled))
                {
                    BangKhenModel BK = new();
                    BK.id = data.id!;
                    BK.number = data.number;
                    BK.fristStation = data.fristStation;
                    BK.lastStation = data.lastStation;
                    BK.roadDesc = data.roadDesc;
                    BK.roadImage = data.roadImage;
                    BK.timeOut = data.timeOut;
        
                    await _AppdbContext.BangKhen.AddRangeAsync(BK);

                    await _AppdbContext.SaveChangesAsync();

                    scope.Complete();
                }

                return new Result
                {
                    success = true,
                    result = "บันทึกข้อมูลสําเร็จ"
                };

            }
            catch (Exception ex)
            {
                throw new Exception("Error on CreateData : " + ex.Message);
            }
        
        }

        public async Task<string?> ValidateData (BangKhen data)
        {
            if(String.IsNullOrEmpty(data.fristStation)) return "กรุณากรอกชื่อสถานีต้นทาง";
            if(String.IsNullOrEmpty(data.lastStation)) return "กรุณากรอกชื่อสถานีปลายทาง";
            if(String.IsNullOrEmpty(data.number)) return "กรุณากรอกเลขรถประจำทาง";
            if(data.timeOut == null || data.timeOut == DateTime.MinValue) return "กรุณากรอกเวลารถออก";

            return null;
            
        }
    }
}