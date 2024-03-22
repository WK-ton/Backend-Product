using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto
{
    public class BangKhen
    {
        public int? id {get; set;} = null;
        public string? number {get; set;} = null;
        public string? fristStation {get; set;} = null;
        public string? lastStation {get; set;} = null;
        public string? roadDesc {get; set;} = null;
        public DateTime? timeOut {get; set;} = null;
        public IFormFile? roadImage {get; set;} = null;
    }
}