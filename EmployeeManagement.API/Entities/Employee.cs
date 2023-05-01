using EmployeeManagement.API.Enums;

namespace EmployeeManagement.API.Entities
{
    /// <summary>
    /// Lấy thông tin nhân viên
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Khai báo kiểu dữ liệu
        /// </summary>
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Fullname { get; set; }
        public Gender Gender { get; set; } //Magic number
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid JobPositionID { get; set; }
        public Guid DepartmentID { get; set; }
        public decimal Salary { get; set; }
        public WorkStatus WorkStatus { get; set; }
        public DateTime JoiningDate { get; set; }
        public string TaxCode { get; set; }

    }
}
