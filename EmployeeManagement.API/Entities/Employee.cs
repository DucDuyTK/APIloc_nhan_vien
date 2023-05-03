using EmployeeManagement.API.Enums;

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
        public string Code { get; set; }
        
        /// <summary>
        /// Tên nhân viên
        /// </summary>
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
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
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
        /// Ngày gia nhập
        /// </summary>
        public DateTime JoiningDate { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string TaxCode { get; set; }

    }
}
