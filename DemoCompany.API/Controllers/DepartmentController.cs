using DemoCompany.BLL;
using DemoCompany.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace DemoCompany.API.Controllers
{
    [ApiController] // sẽ là 1 nhãn dán để .net phân biệt được đây là cái gì
    [Route("api/[controller]")] // http://localhost:3000/api/Department
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost]
        [Route("new-department")] // http://localhost:3000/api/Department/new-department
        public ActionResult<CreateDepartmentResponseDTO> CreateDepartment([FromBody] CreateDepartmentRequestDTO request)
        {
            CreateDepartmentResponseDTO? result = _departmentService.CreateDepartment(request);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
