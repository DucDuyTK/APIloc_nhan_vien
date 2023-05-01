using EmployeeManagement.API.Enums;

namespace EmployeeManagement.API.Entities
{
    /// <summary>
    /// Lấy thông tin nhân viên
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Khai báo kiểu dữ liệu Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Khai báo kiểu dũ liệu Code
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Khai báo kiểu sữ liệu Fullname
        /// </summary>
        public string Fullname { get; set; }

        /// <summary>
        /// Khai báo kiểu sữ liệu Gender
        /// </summary>
        public Gender Gender { get; set; } //Magic number

        /// <summary>
        /// Khai báo kiểu sữ liệu DateOfBirth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Khai báo kiểu sữ liệu PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Khai báo kiểu sữ liệu Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Khai báo kiểu sữ liệu JobPositionId
        /// </summary>
        public Guid JobPositionId { get; set; }

        /// <summary>
        /// Khai báo kiểu sữ liệu DepartmentId
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Khai báo kiểu sữ liệu Salary
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Khai báo kiểu sữ liệu WorkStatus
        /// </summary>
        public WorkStatus WorkStatus { get; set; }

        /// <summary>
        /// Khai báo kiểu sữ liệu JoiningDate
        /// </summary>
        public DateTime JoiningDate { get; set; }

        /// <summary>
        /// Khai báo kiểu sữ liệu TaXCode
        /// </summary>
        public string TaxCode { get; set; }

    }
}
