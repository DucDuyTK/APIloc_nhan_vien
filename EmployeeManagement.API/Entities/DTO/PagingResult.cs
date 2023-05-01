namespace EmployeeManagement.API.Entities.DTO
{
    /// <summary>
    /// Du lieu tra ve api phan trang
    /// </summary>
    public class PagingResult
    {
        /// <summary>
        /// Danh sach nhan vien
        /// </summary>
        public List<Employee> Data { get; set; }

        /// <summary>
        /// Tong so ban ghi thoa man DK
        /// </summary>
        public int TotalRecords { get; set; }
    }
}
