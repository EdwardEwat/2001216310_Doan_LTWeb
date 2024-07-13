using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _2001216310_NguyenThanhVi.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được để trống !!")]
        [RegularExpression("^[A-Za-z0-9]*$", ErrorMessage = "Không được thêm ký tự đặc biệt !!")]
        [MinLength(5, ErrorMessage = "Tối thiểu phải là 5 ký tự !!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống !!")]
        [MinLength(5, ErrorMessage = "Tối thiểu phải là 5 ký tự !!")]
        [RegularExpression("^[A-Za-z0-9]*$", ErrorMessage = "Không được chứa ký tự đặc biệt !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Tên hiển thị không được để trống !!")]
        [RegularExpression("^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆfFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTuUùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ0-9]*$", ErrorMessage = "Tên hiển thị không được có ký tự đặc biệt !!")]
        [MinLength(5, ErrorMessage = "Tối thiểu phải là 5 ký tự !!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Địa chỉ email không được để trống !!")]
        [RegularExpression("^[A-Za-z0-9](\\.?[A-Za-z0-9]){1,}@g(oogle)?mail\\.com$", ErrorMessage = "Không được thêm ký tự đặc biệt ngoài @ và . !!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống !!")]
        [RegularExpression(@"^\(?[0]{1}\)??([0-9]{10})$", ErrorMessage = "Số điện thoại chỉ nhập được là số !!")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống !!")]
        [RegularExpression("^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆfFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTuUùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ0-9,. ]*$", ErrorMessage = "Địa chỉ không được có ký tự đặc biệt !!")]
        public string Address { get; set; }
        public string Img { get; set; }
        public Nullable<double> Salary { get; set; }
        public bool Islocked { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsGuest { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}