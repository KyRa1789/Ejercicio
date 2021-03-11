using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueltoApi.Dto
{
    public class ChangeDetailDto
    {

        public Boolean IsCoin { get; set; }
        public decimal Value { get; set; }
        public int Qty { get; set; }
        public decimal DetailTotal { get; set; }
    }
}
