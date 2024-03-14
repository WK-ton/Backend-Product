using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using api.Repository.Interface;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _IProductRepository;
        public ProductController(IProductRepository IProductRepository)
        {
            _IProductRepository = IProductRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var result = await _IProductRepository.AddProduct(product);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _IProductRepository.GetAllProducts();
            return Ok(result);
        }
    }
}
