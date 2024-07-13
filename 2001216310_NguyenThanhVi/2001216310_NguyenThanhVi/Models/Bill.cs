using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _2001216310_NguyenThanhVi.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<double> Total { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsPay { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> AccountId { get; set; }

        public virtual Account Accounts { get; set; }
        public virtual Store Stores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Buying> Buyings { get; set; }
    }
}