using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _2001216310_NguyenThanhVi.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống !!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}