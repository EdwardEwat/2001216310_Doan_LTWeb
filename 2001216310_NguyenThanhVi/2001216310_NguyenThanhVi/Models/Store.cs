using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _2001216310_NguyenThanhVi.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Locate { get; set; }
        public string OwnerStore { get; set; }
        public string Img { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Owner> Owners { get; set; }
        public virtual ICollection<Storage> Storages { get; set; }
    }
}