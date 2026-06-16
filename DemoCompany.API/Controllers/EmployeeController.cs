using DemoCompany.BLL;
using DemoCompany.BLL.DTO;
using DemoCompany.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoCompany.API.Controllers
{
    // ĐỂ CONTROLLER CÓ THỂ SHOW RA ĐƯỢC BROWSER CLIENT, REACT, AE FRONTEND THÌ BẮT BUỘC CONTROLLER PHẢI TUÂN THỦ NHỮNG QUY TẮC VỀ CONTROLLER CỦA .NET
    [ApiController] // 1 attribute để thông báo cho .net về tác dụng của 1 biến hay 1 hàm nào đó, hay là 1 class, bất kể thành phần muốn khai báo cho .net thì phải dán attribute (nhãn dán) cho .net hiểu
    [Route("api/[controller]")] // biến controller này sẽ lấy cái tên class controller, xóa chữ controller đằng sau lấy cái tên thật gắn vào trong cái biến đó
    // lúc này đường dẫn api của ae mình là: http://localhost:<port>/api/Employee
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        [HttpGet] // phương thức http: mình có tổng cộng 7 phương thức: get, post, put, delete, update, patch, head 
        [Route("employees")]
        public ActionResult<List<GetEmployeeResponseDTO>> GetEmployees()
        {
            List<GetEmployeeResponseDTO> result = _service.GetEmployees();

            return Ok(result);

        }
    }
}
