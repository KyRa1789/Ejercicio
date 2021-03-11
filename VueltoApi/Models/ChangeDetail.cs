using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VueltoApi.Models
{
    public class ChangeDetail
    {
        public int Id { get; set; }
        [ForeignKey("Sale")]
        public int SaleId { get; set; }       
        public Boolean IsCoin { get; set; }
        public decimal Value { get; set; }
        public int Qty { get; set; }
        public decimal DetailTotal { get; set; }
    }
}
