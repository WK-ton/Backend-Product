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


namespace api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
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
    }
}