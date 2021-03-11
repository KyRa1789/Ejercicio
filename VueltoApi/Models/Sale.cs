using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueltoApi.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Change { get; set; }
        public virtual ICollection<ChangeDetail> ChangeDetails { get; set; }
    }
}
