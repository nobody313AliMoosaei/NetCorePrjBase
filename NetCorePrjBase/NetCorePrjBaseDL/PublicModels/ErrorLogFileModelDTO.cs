using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePrjBase.DL.PublicModels
{
    public class ErrorLogFileModelDTO
    {
        public string Key { get; set; }
        public string? Message { get; set; }
        public string? IPAddress { get; set; }
        public string? RequestUrl { get; set; }
        public string? InnerMessage { get; set; }
        public string DateTime { get; set; } 
    }
}
