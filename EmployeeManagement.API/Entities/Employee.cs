using EmployeeManagement.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.Entities
{
    /// <summary>
    /// Lấy thông tin nhân viên
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required(ErrorMessage = "Mã nhân viên không được để trống")]
        [MaxLength(20, ErrorMessage = "Mã nhân viên không được vượt quá 20 ký tự")]
        public string Code { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [Required(ErrorMessage = "Tên nhân viên không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên nhân viên không được vượt quá 100 ký tự")]
        public string Fullname { get; set; }

        /// <summary>
        /// Giới tính: 0 là nam, 1 là nữ, 2 là khác
        /// </summary>
        public Gender Gender { get; set; } //Magic number

        /// <summary>
        /// Ngày, tháng, năm sinh
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Required]
        [MaxLength (25)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Email không được để trống")]
        [MaxLength(50, ErrorMessage = "Email không được để quá 50 lý tự")]
        [EmailAddress(ErrorMessage = "Email chưa đúng định dạng")]
        public string Email { get; set; }

        /// <summary>
        /// Khóa ngoại Id vị trí
        /// </summary>
        public Guid JobPositionId { get; set; }

        /// <summary>
        /// Khóa ngoại Id phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Lương
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Trạng thái công việc: 1 là đang thử việc, 2 là đang làm việc, 2 là đã nghỉ việc
        /// </summary>
        public WorkStatus WorkStatus { get; set; }

        /// <summary>
        /// Số CMT/ CCCD
        /// </summary>
        [Required(ErrorMessage = "Căn cước công dân không được để trống")]
        [MaxLength(25, ErrorMessage = "Căn cước công dân không được để quá 25 lý tự")]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp CMT/ CCCD
        /// </summary>
        public DateTime IdentityIssuerDate { get; set; }

        /// <summary>
        /// Nơi cấp CMT/ CCCD
        /// </summary>
        public string IdentityIssuerPlace { get; set; }

        /// <summary>
        /// Ngày gia nhập
        /// </summary>
        public DateTime JoiningDate { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string TaxCode { get; set; }
        
    }
}
