using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _2001216310_NguyenThanhVi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Nullable<double> BfDiscount { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public Nullable<int> PromotionId { get; set; }
        public Nullable<int> BrandId { get; set; }
        public string Img { get; set; }

        public virtual Brand Brand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Buying> Buyings { get; set; }
        public virtual Promotion Promotions { get; set; }
        public virtual Storage Storages { get; set; }
    }
}