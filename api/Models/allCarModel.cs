using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class BangKhenModel
    {
        public int? id {get; set;} = null;
        public string? number {get; set;} = null;
        public string? firstStation {get; set;} = null;
        public string? lastStation {get; set;} = null;
        public string? roadDesc {get; set;} = null;
        public DateTime? timeOut {get; set;} = null;
        public string? roadImage {get; set;} = null;
    }
    public class MorchitModel
    {
        public int? id {get; set;} = null;
        public string? number {get; set;} = null;
        public string? firstStation {get; set;} = null;
        public string? lastStation {get; set;} = null;
        public string? roadDesc {get; set;} = null;
        public DateTime? timeOut {get; set;} = null;
        public string? roadImage {get; set;} = null;
    }

    public class MonumentModel
    {
        public int? id {get; set;} = null;
        public string? number {get; set;} = null;
        public string? firstStation {get; set;} = null;
        public string? lastStation {get; set;} = null;
        public string? roadDesc {get; set;} = null;
        public DateTime? timeOut {get; set;} = null;
        public string? roadImage {get; set;} = null;
    }
}