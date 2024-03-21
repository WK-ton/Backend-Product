using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}
        public DbSet<SignUp> signUp {get; set;} 
        public DbSet<BangKhenModel> BangKhen {get; set;}
        public DbSet<SaveOTP> saveOtp {get; set;} 
        
    }
}