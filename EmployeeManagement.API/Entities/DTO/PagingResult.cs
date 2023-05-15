namespace EmployeeManagement.API.Entities.DTO
{
    /// <summary>
    /// Du lieu tra ve api phan trang
    /// </summary>
    public class PagingResult
    {
        internal int totalRecords;

        /// <summary>
        /// Danh sach đối tượng trả về trên 1 trang
        /// </summary>
        public List<object> Data { get; set; }

        /// <summary>
        /// Tong so ban ghi thoa man DK
        /// </summary>
        public int TotalRecords { get; set; }
    }
}
