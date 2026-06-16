using DemoCompany.BLL.DTO;
using DemoCompany.DAL;
using DemoCompany.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCompany.BLL
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _empRepo;

        public EmployeeService(EmployeeRepository empRepo)
        {
            _empRepo = empRepo;
        }

        public List<GetEmployeeResponseDTO> GetEmployees()
        {

            return _empRepo.GetEmployees().Select(e => new GetEmployeeResponseDTO
            {
                Id = e.EmployeeId,
                EmployeeName = e.FullName,
                DepartmentName = e.Department!.DepartmentName
            }).ToList();  
        }


    }
}
