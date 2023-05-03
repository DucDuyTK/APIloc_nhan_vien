namespace EmployeeManagement.API.Entities
{
    public class JobPosition
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Mã vị trí
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string Name { get; set; }
    }
}
