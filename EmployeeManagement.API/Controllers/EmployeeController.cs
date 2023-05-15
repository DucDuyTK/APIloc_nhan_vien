using Dapper;
using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Entities.DTO;
using EmployeeManagement.API.Enums;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Reflection.Metadata;

namespace EmployeeManagement.API.Controllers
{
    /// <summary>
    /// Các api liên quan đến nhân viên
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        /*public int GetTotalRecords()
        {
            return TotalRecords;
        }*/

        /// <summary>
        /// API lấy danh sách nhân viên theo điều kiện lọc và phân trang
        /// </summary>
        /// <param name="keyword"> Từ khóa tìm kiếm (Mã nhân viên, Tên, Số điện thoại,...) </param>
        /// <param name="jobPositionId"> Id vị trí </param>
        /// <param name="deparmentId"> Id phòng ban </param>
        /// <param name="limit"> Số bản ghi muốn lấy </param>
        /// <param name="ofset"> Vị trí bản ghi bắt đầu lấy </param>
        /// <returns>
        /// Trả về 1 đối tượng PagingResult danh sách nhân viên
        /// trong 1 trang và tổng số bản ghi thỏa mãn điều kiện
        /// </returns>
        [HttpGet]
        public IActionResult GetPaging(
            [FromQuery] string? keyword,
            [FromQuery] Guid? jobPositionId,
            [FromQuery] Guid? departmentId,
            [FromQuery] int limit = 20,
            [FromQuery] int offset = 0)
        {
            try
            {
                string storedProcedureName = "Proc_Employee_GetPaging";

                var parameters = new DynamicParameters();
                parameters.Add("p_Keyword", keyword);
                parameters.Add("p_JobPositionId", jobPositionId);
                parameters.Add("p_DepartmentId", departmentId);
                parameters.Add("p_Limit", limit);
                parameters.Add("p_Offset", offset);

                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

                var multipleResultSets = mySqlConnection.QueryMultiple(
                    storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                var employees = multipleResultSets.Read<object>().ToList();

                int totalRecords = multipleResultSets.Read<int>().FirstOrDefault();

                return Ok(new PagingResult
                {
                    Data = employees,
                    TotalRecords = totalRecords
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfo = "https://google.com",
                    TraceId = HttpContext.TraceIdentifier
                });
            }

            /* return new PagingResult
            {
                Data = new List<object>
                {
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        Code = "NV001",
                        Fullname = "Nguyễn Đức Duy",
                        Gender = Enums.Gender.Male,
                        DateOfBirth = new DateTime(),
                        PhoneNumber = "0987654321",
                        Email = "nguyenducduy@gmail.com",
                        JobPositionId= Guid.NewGuid(),
                        DepartmentId= Guid.NewGuid(),
                        Salary=12323413,
                        WorkStatus=WorkStatus.TrialJob,
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
                        Email = "khuatthithaonguyen@gmail.com",
                        JobPositionId= Guid.NewGuid(),
                        DepartmentId= Guid.NewGuid(),
                        Salary=2398471,
                        WorkStatus=WorkStatus.Working,
                        JoiningDate=DateTime.Now,
                        TaxCode= "28731921"
                    },
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        Code = "NV003",
                        Fullname = "Phạm Tuấn Duy",
                        Gender = Enums.Gender.Other,
                        DateOfBirth = new DateTime(),
                        PhoneNumber = "098767686",
                        Email = "phamtuanduy@gmail.com",
                        JobPositionId= Guid.NewGuid(),
                        DepartmentId= Guid.NewGuid(),
                        Salary=21374821,
                        WorkStatus=WorkStatus.LeaveOffWork,
                        JoiningDate=DateTime.Now,
                        TaxCode = "27312774"
                    }
                },
                TotalRecords = 3
            };*/
        }

        /// <summary>
        /// API lấy chi tiết 1 nhân viên
        /// </summary>
        /// <param name="employeeId"> Id nhân viên </param>
        /// <returns> Trả về chi tiết thông tin nhân viên muốn lấy </returns>
        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeById(
            [FromRoute] Guid employeeId)
        {
            try// Ctrl + k + s
            {
                // Chuẩn bị Stored Proceduce
                string storedProceduceName = "Proc_employee_GetById";

                // Chuẩn bị tham số đầu vào cho Stored
                var parameters = new DynamicParameters(); // Anonymous type (đại diện cho all các kiểu dữ liệu)
                parameters.Add("p_Id", employeeId);

                // Khởi tạo kết nối của Database
                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

                // Thực hiện gọi vào Database để chạy stored proceduce  // Extention method
                var employee = mySqlConnection.QueryFirstOrDefault<Employee>
                    (storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (employee == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(employee);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfo = "https://google.com",
                    TraceId = HttpContext.TraceIdentifier
                });
            }

            // Try catch dể bắt exception

            /*
            return Ok(new Employee
            {
                Id = employeeId,
                Code = "NV001",
                Fullname = "Nguyễn Đức Duy",
                Gender = Enums.Gender.Male,
                DateOfBirth = new DateTime(),
                PhoneNumber = "0987654321",
                Email = "nguyenducduy@gmail.com",
                JobPositionId = Guid.NewGuid(),
                DepartmentId = Guid.NewGuid(),
                Salary = 12323413,
                WorkStatus = WorkStatus.TrialJob,
                JoiningDate = DateTime.Now,
                TaxCode = "27193343"
            });
            */
        }

        /// <summary>
        /// API lấy mã nhân viên tự động tăng
        /// </summary>
        /// <returns> Mã nhân viên tự động tăng </returns>
        [HttpGet("new-Code")]
        public IActionResult GetNewEmployeeCode()
        {
            return Ok("NV22222");
        }

        /// <summary>
        /// API thêm mới nhân viên
        /// </summary>
        /// <param name="newEmployee"> Id nhân viên cần thêm mới </param>
        /// <returns> Trả về Id nhân viên vừa thêm mới thành công </returns>
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee newEmployee)
        {
            try
            {
                //validate

                //lấy được toàn bộ thuộc tính của class employee             
                var properties = typeof(Employee).GetProperties();

                //Khai báo mảng lỗi
                var validateFailures = new List<string>();

                foreach (var property in properties)
                {
                    string propertyName = property.Name;

                    //Kiểm tra xem thuộc tính đó có attribute  required hay không
                    var requiredAttribute = (RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();
                    if (requiredAttribute != null)
                    {
                        if (String.IsNullOrEmpty(property.GetValue(newEmployee).ToString()))
                        {
                            validateFailures.Add(requiredAttribute.ErrorMessage);
                        }
                    }
                    //Kiểm tra xem thuộc tính đó có maxLength required hay không
                    var maxlengthAttribute = (MaxLengthAttribute)property.GetCustomAttributes(typeof(MaxLengthAttribute), false).FirstOrDefault();
                    if (maxlengthAttribute != null)
                    {
                        if (property.GetValue(newEmployee).ToString().Length > maxlengthAttribute.Length)
                        {
                            validateFailures.Add(maxlengthAttribute.ErrorMessage);
                        }
                    }
                    //Kiểm tra xem thuộc tính đó có EmailAddress required hay không
                    var emailAddressAttribute = (EmailAddressAttribute)property.GetCustomAttributes(typeof(EmailAddressAttribute), false).FirstOrDefault();
                    if (emailAddressAttribute != null)
                    {
                        if (!String.IsNullOrEmpty(property.GetValue(newEmployee)?.ToString()))
                        {
                            validateFailures.Add(emailAddressAttribute.ErrorMessage);
                        }
                    }
                }

                //Kiểm tra mảng lỗi xem có phần tử nào không và return về lỗi
                if (validateFailures.Count > 0)
                {
                    return BadRequest(new ErrorResult
                    {
                        ErrorCode = Enums.ErrorCode.InvalidData,
                        DevMsg = "",
                        UserMsg = "",
                        MoreInfo = validateFailures,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }

                //Chuẩn bị tên store procedure
                string storedProcedureName = "Proc_Employee_Insert";

                //Chuẩn bị tham số đầu vào cho store
                var parameters = new DynamicParameters();
                var newId = Guid.NewGuid();
                parameters.Add("p_Id", newId);
                parameters.Add("p_Code", newEmployee.Code);
                parameters.Add("p_FullName", newEmployee.Fullname);
                parameters.Add("p_Gender", newEmployee.Gender);
                parameters.Add("p_DateOfBirth", newEmployee.DateOfBirth);
                parameters.Add("p_PhoneNumber", newEmployee.PhoneNumber);
                parameters.Add("p_Email", newEmployee.Email);
                parameters.Add("p_JobPositionID", newEmployee.JobPositionId);
                parameters.Add("p_DepartmentID", newEmployee.DepartmentId);
                parameters.Add("p_Salary", newEmployee.Salary);
                parameters.Add("p_WorkStatus", newEmployee.WorkStatus);
                parameters.Add("p_IdentityNumber", newEmployee.IdentityNumber);
                parameters.Add("p_IdentityIssuerDate", newEmployee.IdentityIssuerDate);
                parameters.Add("p_IdentityIssuerPlace", newEmployee.IdentityIssuerPlace);
                parameters.Add("p_TaxCode", newEmployee.TaxCode);
                parameters.Add("p_JoiningDate", newEmployee.JoiningDate);

                //Khởi tạo kết nối tới Database
                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

                //Thực hiện gọi vào database để chạy stored procedure
                int numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                //Xử lý kết quả trả về	
                if (numberOfAffectedRows == 1)
                {
                    return StatusCode(StatusCodes.Status201Created, newId);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.DatabaseFailed,
                    DevMsg = Resource.DevMsg_DatabaseFailed,
                    UserMsg = Resource.UserMsg_DatabaseFailed,
                    MoreInfo = "https://google.com",
                    TraceId = HttpContext.TraceIdentifier
                });
            }

            //Try catch để bắt exception
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfo = "https://google.com",
                    TraceId = HttpContext.TraceIdentifier

                });
            }
        }

        /// <summary>
        /// API sửa thông tin nhân viên
        /// </summary>
        /// <param name="updateEmployee"> Thông tin cần sửa của nhân viên </param>
        /// <param name="employeeId"> Id nhân viên cần sửa </param>
        /// <returns> Trả về ID của nhân viên vừa mới sửa </returns>
        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployee(
            [FromBody] Employee updateEmployee,
            [FromRoute] Guid employeeId)
        {
            try
            {
                //validate

                //lấy được toàn bộ thuộc tính của class employee             
                var properties = typeof(Employee).GetProperties();

                //Khai báo mảng lỗi
                var validateFailures = new List<string>();

                foreach (var property in properties)
                {
                    string propertyName = property.Name;

                    //Kiểm tra xem thuộc tính đó có attribute  required hay không
                    var requiredAttribute = (RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();
                    if (requiredAttribute != null)
                    {
                        if (String.IsNullOrEmpty(property.GetValue(updateEmployee).ToString()))
                        {
                            validateFailures.Add(requiredAttribute.ErrorMessage);
                        }
                    }
                    //Kiểm tra xem thuộc tính đó có maxLength required hay không
                    var maxlengthAttribute = (MaxLengthAttribute)property.GetCustomAttributes(typeof(MaxLengthAttribute), false).FirstOrDefault();
                    if (maxlengthAttribute != null)
                    {
                        if (property.GetValue(updateEmployee).ToString().Length > maxlengthAttribute.Length)
                        {
                            validateFailures.Add(maxlengthAttribute.ErrorMessage);
                        }
                    }
                    //Kiểm tra xem thuộc tính đó có EmailAddress required hay không
                    var emailAddressAttribute = (EmailAddressAttribute)property.GetCustomAttributes(typeof(EmailAddressAttribute), false).FirstOrDefault();
                    if (emailAddressAttribute != null)
                    {
                        if (String.IsNullOrEmpty(property.GetValue(updateEmployee)?.ToString()))
                        {
                            validateFailures.Add(emailAddressAttribute.ErrorMessage);
                        }
                    }
                }

                //Kiểm tra mảng lỗi xem có phần tử nào không và return về lỗi
                if (validateFailures.Count > 0)
                {
                    return BadRequest(new ErrorResult
                    {
                        ErrorCode = Enums.ErrorCode.InvalidData,
                        DevMsg = "",
                        UserMsg = "",
                        MoreInfo = validateFailures,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }

                //Chuẩn bị tên store procedure
                string storedProcedureName = "Proc_Employee_Update";

                //Chuẩn bị tham số đầu vào cho store
                var parameters = new DynamicParameters();
                parameters.Add("p_Id", employeeId);
                parameters.Add("p_Code", updateEmployee.Code);
                parameters.Add("p_FullName", updateEmployee.Fullname);
                parameters.Add("p_Gender", updateEmployee.Gender);
                parameters.Add("p_DateOfBirth", updateEmployee.DateOfBirth);
                parameters.Add("p_PhoneNumber", updateEmployee.PhoneNumber);
                parameters.Add("p_Email", updateEmployee.Email);
                parameters.Add("p_JobPositionID", updateEmployee.JobPositionId);
                parameters.Add("p_DepartmentID", updateEmployee.DepartmentId);
                parameters.Add("p_Salary", updateEmployee.Salary);
                parameters.Add("p_WorkStatus", updateEmployee.WorkStatus);
                parameters.Add("p_IdentityNumber", updateEmployee.IdentityNumber);
                parameters.Add("p_IdentityIssuerDate", updateEmployee.IdentityIssuerDate);
                parameters.Add("p_IdentityIssuerPlace", updateEmployee.IdentityIssuerPlace);
                parameters.Add("p_TaxCode", updateEmployee.TaxCode);
                parameters.Add("p_JoiningDate", updateEmployee.JoiningDate);

                //Khởi tạo kết nối tới Database
                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

                //Thực hiện gọi vào database để chạy stored procedure
                int numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                //Xử lý kết quả trả về	
                if (employeeId != null)
                {
                    return Ok(employeeId);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.DatabaseFailed,
                    DevMsg = Resource.DevMsg_DatabaseFailed,
                    UserMsg = Resource.UserMsg_DatabaseFailed,
                    MoreInfo = "https://google.com",
                    TraceId = HttpContext.TraceIdentifier
                });
            }

            //Try catch để bắt exception
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfo = "https://google.com",
                    TraceId = HttpContext.TraceIdentifier

                });
            }
        }

        /// <summary>
        /// API xóa nhân viên
        /// </summary>
        /// <param name="employeeId"> Id của nahan viên muốn xóa </param>
        /// <returns> Trả về Id nhân viên vừa mới xóa </returns>
        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee(
            [FromRoute] Guid employeeId)
        {
            try
            {
                // Chuẩn bị Stored Proceduce
                string storedProcedureName = "Proc_Employee_Delete";

                // Chuẩn bị tham số đầu vào cho Stored
                var parameters = new DynamicParameters();
                parameters.Add("p_Id", employeeId);

                // Khởi tạo kết nối của Database
                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

                // Thực hiện gọi vào Database để chạy stored proceduce
                int numberOfAffectedRows = mySqlConnection.Execute
                    (storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (numberOfAffectedRows == 1)
                {
                    return Ok(employeeId);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Enums.ErrorCode.DatabaseFailed,
                        DevMsg = Resource.DevMsg_DatabaseFailed,
                        UserMsg = Resource.UserMsg_DatabaseFailed,
                        MoreInfo = "https://google.com",
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfo = "https://google.com",
                    TraceId = HttpContext.TraceIdentifier
                });
                
            }

            //return Ok(employeeId);
        }


    }
}
