using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _2001216310_NguyenThanhVi.Models
{
    public class Storage
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public Nullable<int> Amount { get; set; }
        public Nullable<int> StoreId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
        public virtual Store Stores { get; set; }
    }
}