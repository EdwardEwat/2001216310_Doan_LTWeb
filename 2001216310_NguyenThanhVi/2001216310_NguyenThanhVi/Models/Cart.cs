using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _2001216310_NguyenThanhVi.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int ProId { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
    }
}