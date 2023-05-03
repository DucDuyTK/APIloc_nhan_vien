using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        /// <summary>
        /// API lấy danh sách phòng ban theo điều kiện và phân trang
        /// </summary>
        /// <param name="keyword"> Từ khóa tìm kiếm (Mã phòng ban, tên phòng ban) </param>
        /// <param name="limit"> số bản ghi muốn lấy </param>
        /// <param name="offset"> Vị trí bản ghi bắt đầu lấy </param>
        /// <returns>
        /// Trả về 1 đối tượng PagingResult danh sách phòng ban
        /// trong 1 trang và tổng số bản ghi thỏa mãn điều kiện
        /// </returns>
        [HttpGet]
        public IActionResult GetPaging(
            [FromQuery] string? keyword,
            [FromQuery] int limit = 20,
            [FromQuery] int offset = 0)
        {
            return Ok(new PagingResult
            {
                Data = new List<object>
                {
                    new Department
                    {
                        Id = Guid.NewGuid(),
                        Code = "PB001",
                        Name = "Phòng quản lý"
                    },
                    new Department
                    {
                        Id = Guid.NewGuid(),
                        Code = "PB002",
                        Name = "Phòng kinh doanh"
                    },
                    new Department
                    {
                        Id = Guid.NewGuid(),
                        Code = "PB003",
                        Name = "Phòng Marketing"
                    }
                },
                TotalRecords = 3
            });
        }
    }
}
