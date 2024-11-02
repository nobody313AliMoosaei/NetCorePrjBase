using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePrjBase.DL.PublicModels
{
    public class MetaDataDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? TotalRow { get; set; } = 0;
        public float? TotalPage { get; set; } = 0;
    }
}
