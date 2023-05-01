using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Entities.DTO;
using EmployeeManagement.API.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    /// <summary>
    /// Các api liên quan đến nhân viên
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// API lấy danh sách nhân viên theo điều kiện lọc và phân trang
        /// </summary>
        /// <param name="keyword"> Từ khóa tìm kiếm (Mã nhân viên, Tên, Số điện thoại,...) </param>
        /// <param name="jobPositionId"> Id vị trí </param>
        /// <param name="deparmentId"> Id phòng ban </param>
        /// <param name="limit"> Số bản ghi muốn lấy </param>
        /// <param name="ofset"> Vị trí bản ghi bắt đầu lấy </param>
        /// <returns>
        /// Trả về 1 đối tượng PagingResult danh sách nhân viên trong 1 trang và tổng số bản ghi thỏa mãn điều kiện
        /// </returns>
        [HttpGet]
        public PagingResult GetPaging(
            [FromQuery]string? keyword,
            [FromQuery]Guid? jobPositionId,
            [FromQuery]Guid? deparmentId,
            [FromQuery]int limit = 20,
            [FromQuery]int ofset = 0)
        {
            return new PagingResult
            {
                Data = new List<Employee>
                {
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        Code = "NV001",
                        Fullname = "Duy Thiếu Hiệp",
                        Gender = Enums.Gender.Male,
                        DateOfBirth = new DateTime(),
                        PhoneNumber = "0987654321",
                        Email = "Ex1@gmail.com",
                        JobPositionID= Guid.NewGuid(),
                        DepartmentID= Guid.NewGuid(),
                        Salary=12323413,
                        WorkStatus=WorkStatus.Dang_thu_viec,
                        JoiningDate=DateTime.Now,
                        TaxCode = "27193343"
                    },
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        Code = "NV002",
                        Fullname = "Khuất Thị Thảo Nguyên",
                        Gender = Enums.Gender.Female,
                        DateOfBirth = new DateTime(),
                        PhoneNumber = "0987372818",
                        Email = "Ex1@gmail.com",
                        JobPositionID= Guid.NewGuid(),
                        DepartmentID= Guid.NewGuid(),
                        Salary=2398471,
                        WorkStatus=WorkStatus.Dang_lam_viec,
                        JoiningDate=DateTime.Now,
                        TaxCode= "28731921"
                    },
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        Code = "NV003",
                        Fullname = "Tớ Hận Cậu",
                        Gender = Enums.Gender.Other,
                        DateOfBirth = new DateTime(),
                        PhoneNumber = "098767686",
                        Email = "Ex3@gmail.com",
                        JobPositionID= Guid.NewGuid(),
                        DepartmentID= Guid.NewGuid(),
                        Salary=21374821,
                        WorkStatus=WorkStatus.Da_nghi_viec,
                        JoiningDate=DateTime.Now,
                        TaxCode = "27312774"
                    }
                },
                TotalRecords = 3
            };
        }
    }
}
