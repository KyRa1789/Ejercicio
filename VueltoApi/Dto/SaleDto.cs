using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueltoApi.Dto
{
    public class SaleDto
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Change { get; set; }
        public virtual List<ChangeDetailDto> ChangeDetails { get; set; }
    }
}
