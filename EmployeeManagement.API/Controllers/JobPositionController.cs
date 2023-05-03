using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JobPositionController : ControllerBase
    {
        /// <summary>
        /// API lấy danh sách vị trí theo điều kiện và phân trang
        /// </summary>
        /// <param name="keyword"> Từ khóa tìm kiếm (Mã vị trí, tên vị trí) </param>
        /// <param name="limit"> Số bản ghi muốn lấy </param>
        /// <param name="offset"> Vị trí bản ghi bắt đầu lấy </param>
        /// <returns>
        /// Trả về 1 đối tượng PagingResult danh sách vị trí
        /// trong 1 trang và số bản ghi thỏa mãn điều kiện
        /// </returns>
        [HttpGet]
        public IActionResult GetingResult(
            [FromQuery] string? keyword,
            [FromQuery] int limit = 20,
            [FromQuery] int offset = 0)
        {
            return Ok(new PagingResult
            {
                Data = new List<object>
                {
                    new JobPosition
                    {
                        Id = Guid.NewGuid(),
                        Code = "VT001",
                        Name = "Tổng giám đôc"
                    },
                    new JobPosition
                    {
                        Id = Guid.NewGuid(),
                        Code = "VT001",
                        Name = "Tổng giám đôc"
                    },
                    new JobPosition
                    {
                        Id = Guid.NewGuid(),
                        Code = "VT001",
                        Name = "Tổng giám đôc"
                    }
                },
                TotalRecords = 3
            });
        }
    }
}
