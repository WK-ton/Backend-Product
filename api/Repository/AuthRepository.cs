using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.EntityFrameworkCore;
using api.Dto;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Transactions;
using Dapper;
using System.Linq;


namespace api.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _appDbContext;

        public AuthRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<Result> AddProduct(Product product)
        {
            try
            {
                _appDbContext.Products.Add(product);
                _appDbContext.SaveChanges();
                return Task.FromResult(new Result
                {
                    success = true,
                    result = product
                });

            }
            catch (Exception ex)
            {
                throw new Exception("Error on AddProduct : " + ex.Message);
            }
        }

        public async Task<Result> GetAllProducts()
        {
            var products = await _appDbContext.Products.ToListAsync();
            return new Result
            {
                success = true,
                result = products
            };
        }

        public async Task<Result> signUp(Register data)
        {
            try
            {
                string? strError = await ValidateData(data);
                if (strError != null)
                {
                    return new Result
                    {
                        success = false,
                        errorMessage = strError
                    };
                }

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    signUpModel S = new();
                    S.id = S.id!;
                    S.name = data.name;
                    S.email = data.email;
                    S.password = data.password;
                    S.passwordRepeat = data.passwordRepeat;
                    S.phone = data.phone;
                    S.image = data.image;
                    await _appDbContext.signUp.AddRangeAsync(S);

                    await _appDbContext.SaveChangesAsync();
                    scope.Complete();
                }

                return new Result
                {
                    success = true,
                    result = "สมัครสมาชิกสำเร็จ"
                };

            }
            catch (Exception ex)
            {
                throw new Exception("Error on signUp : " + ex.Message);
            }
        }

        public async Task<string?> ValidateData(Register data)
        {
            var phoneRegex = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");


            if (string.IsNullOrEmpty(data.name)) return "กรุณาใส่ชื่อให้ถูกต้อง";
            if (string.IsNullOrEmpty(data.email) || !emailRegex.IsMatch(data.email)) return "กรุณาใส่อีเมลให้ถูกต้อง";
            if (string.IsNullOrEmpty(data.password) || string.IsNullOrEmpty(data.passwordRepeat)) return "กรุณาใส่รหัสผ่านให้ถูกต้อง";
            if (data.password.Length < 6) return "กรุณากรอกรหัสผ่านอย่างน้อย 6 ตัวอักษร";
            if (data.passwordRepeat.Length < 6) return "กรุณากรอกรหัสผ่านอย่างน้อย 6 ตัวอักษร";
            if (data.password != data.passwordRepeat) return "รหัสผ่านของคุณไม่ตรงกัน";
            if (string.IsNullOrEmpty(data.phone) || !phoneRegex.IsMatch(data.phone)) return "หมายเลขโทรศัพท์ของคุณไม่ถูกต้อง";
            if (data.phone.Length != 10) return "กรุณากรอกเบอร์โทรศัพท์ให้ครบ 10 ตำแหน่ง";

            
            var email = await CheckerData(data.email, null);
            if (email == true) return "อีเมลใช้งานแล้ว";

            var phone = await CheckerData(null, data.phone);
            if (phone == true) return "เบอร์โทรศัพท์ใช้งานแล้ว";
            

            return null;
        }

        public async Task<bool> CheckerData (string email, string phone)
        {
            var res = await _appDbContext.signUp.FirstOrDefaultAsync(u => u.email == email || u.phone == phone);
            return res != null;
        }

        public async Task<Result> login(Login data)
        {
            try
            {
                string? strError = await ValidateData(data);
                if (strError != null)
                {
                    return new Result
                    {
                        success = false,
                        errorMessage = strError
                    };
                }
                    
                
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error on login : " + ex.Message);
            }
        }
    }
}