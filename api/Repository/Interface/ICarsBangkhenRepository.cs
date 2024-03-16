using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;

namespace api.Repository.Interface
{
    public interface ICarsBangkhenRepository
    {
        Task<Result> CreateData (BangKhen data);
             
    }
}