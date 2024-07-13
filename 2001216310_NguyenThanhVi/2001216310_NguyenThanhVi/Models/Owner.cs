using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _2001216310_NguyenThanhVi.Models
{
    public class Owner
    {
        [Key]   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public Nullable<int> StoreId { get; set; }

        public virtual Store Stores { get; set; }
    }
}