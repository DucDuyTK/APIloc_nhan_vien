using EmployeeManagement.API.Enums;

namespace EmployeeManagement.API.Entities.DTO
{
    public class ErrorResult
    {
        /// <summary>
        /// Định dạng nội bộ
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Thông báo lỗi cho Dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Thông báo lỗi cho User
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Đường dẫn mở chi tiết hơn về lỗi
        /// </summary>
        public object MoreInfo { get; set; }

        /// <summary>
        /// Mã tra cứu thông tin log
        /// </summary>
        public string TraceId { get; set; }
    }
}
