using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _2001216310_NguyenThanhVi.Models
{
    public class Buying
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> BillId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> Amount { get; set; }

        public virtual Bill Bills { get; set; }
        public virtual Product Products { get; set; }
    }
}