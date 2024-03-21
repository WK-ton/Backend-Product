using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using api.Data;
using api.Dto;
using api.Models;
using api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class OtpRepository : IOtpRepository
    {
        private readonly AppDbContext _appDbContext;

        public OtpRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Result> sendOTP(SendOTP data)
        {
            try
            {
                Random otp = new Random();

                var phoneRegex = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");

                if (String.IsNullOrEmpty(data.phone) || !phoneRegex.IsMatch(data.phone))
                    return new Result
                    {
                        result = "หมายเลขโทรศัพท์ของคุณไม่ถูกต้อง"
                    };
                if (data.phone.Length != 10)
                    return new Result
                    {
                        result = "กรุณากรอกเบอร์โทรศัพท์ให้ครบ 10 ตำแหน่ง"
                    };

                var checkPhone = await _appDbContext.signUp.FirstOrDefaultAsync(s => s.phone == data.phone);
                if (checkPhone == null)
                {
                    SaveOTP o = new();
                    o.id = data.id!;
                    o.phone = data.phone!;
                    o.otp = otp.Next(100000, 999999).ToString();

                    await _appDbContext.saveOtp.AddAsync(o);

                    await _appDbContext.SaveChangesAsync();
                }
                else
                {
                    return new Result
                    {
                        success = false,
                        errorMessage = "เบอร์โทรศัพท์มีอยู่ในระบบแล้ว"
                    };
                }

                return new Result
                {
                    success = true,
                    result = "ส่งรหัส OTP สําเร็จ"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error on SendOTP : " + ex.Message);
            }
        }


        public async Task<Result> activeOTP(ActiveOTP data)
        {
            try
            {

                var result = await _appDbContext.saveOtp.FirstOrDefaultAsync(o => o.id == data.id && o.otp == data.otp);
                if (result != null)
                {
                    // if (String.IsNullOrEmpty(data.phone)) return new Result
                    // {
                    //     success = false,
                    //     errorMessage = "กรุณาระบุเบอร์โทรศัพท์"
                    // };

                    var checkPhone = await _appDbContext.signUp.FirstOrDefaultAsync(s => s.phone == result.phone);
                    if (checkPhone == null)
                    {
                        SignUp a = new();
                        a.id = a.id!;
                        a.phone = result.phone!;

                        await _appDbContext.signUp.AddAsync(a);
                        await _appDbContext.SaveChangesAsync();
                    }
                    else
                    {
                        return new Result
                        {
                            success = false,
                            errorMessage = "เบอร์โทรศัพท์มีอยู่ในระบบแล้ว"
                        };
                    }

                    return new Result
                    {
                        success = true,
                        result = "ยืีนยัน OTP สําเร็จ"
                    };
                }
                else
                {
                    return new Result
                    {
                        success = false,
                        errorMessage = "รหัส OTP ไม่ถูกต้อง"
                    };
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error on SendOTP : " + ex.Message);
            }
        }
    }
}