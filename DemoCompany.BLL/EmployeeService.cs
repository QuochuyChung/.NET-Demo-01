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
        private readonly DepartmentRepository _depRepo;
        private readonly UnitOfWork _unitOfWork;

        /*
         Service là nơi chuyển giao từ request -> entity -> Database -> response
         DbContext context = new DbContext();
         */

        public EmployeeService(EmployeeRepository empRepo, DepartmentRepository depRepo, UnitOfWork unitOfWork)
        {
            _empRepo = empRepo;
            _depRepo = depRepo;
            _unitOfWork = unitOfWork;
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

        public CreateEmployeeResponseDTO? CreateEmployee(CreateEmployeeRequestDTO request)
        {
            // validate đầu vào

            #region Validate dữ liệu
            
            // Validate đầu vào của fullname và email
            if (string.IsNullOrEmpty(request.FullName) || string.IsNullOrEmpty(request.Email))
            {
                return null;
            }

            // Validate salary
            if (request.Salary <= 0)
            {
                return null;
            }

            Department? department = _depRepo.GetDepartmentById(request.DepartmentId);
            
            if(department == null)
            {
                return null;
            }

            Employee? existEmp = _empRepo.GetByCondition(e => e.Email == request.Email || e.Phone == request.PhoneNumber);

            if (existEmp != null)
            {
                return null;
            }
            #endregion


            // chuyển thành entity
            #region Chuyển thành Entity
            Employee newEmployee = new Employee
            {
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.PhoneNumber,
                Position = request.Location,
                Salary = request.Salary,    
                HireDate = DateOnly.FromDateTime(DateTime.Now),
                DepartmentId = request.DepartmentId,
                IsActive = true
            };
            #endregion

            // lưu trên ram
            bool result =  _empRepo.CreateEmployee(newEmployee);

            if (!result)
            {
                return null;
            }

            _unitOfWork.SaveChanges();
           
            // trả ra response
            CreateEmployeeResponseDTO response = new CreateEmployeeResponseDTO
            {
                Email = newEmployee.Email,
                FullName = newEmployee.FullName,
                HireDate = newEmployee.HireDate?.ToDateTime(TimeOnly.MinValue),
                PhoneNumber = newEmployee.Phone!
            };

            return response;
        }

        // SOFT VÀ HARD DELETE
        /*
         + SOFT DELETE: IsActived, IsDeleted - IMPLEMENT THEO KIỂU NÀY
         + HARD DELETE: Xóa thật, xóa ra khỏi DB
         */

        public DeleteEmployeeResponseDTO? SoftDeleteEmployee(DeleteEmployeeRequestDTO request)
        {
            Employee? existEmployee = _empRepo.GetEmployeeById(request.EmployeeID);

            if (existEmployee == null)
            {
                return null;
            }

             _empRepo.SoftDelete(existEmployee.EmployeeId);

            int deleteResult =  _unitOfWork.SaveChanges();

            if (deleteResult <= 0)
            {
                return new DeleteEmployeeResponseDTO()
                {
                    EmployeeName = existEmployee.FullName,
                    Message = "Xóa không thành công "
                };
            };


            DeleteEmployeeResponseDTO response = new DeleteEmployeeResponseDTO
            {
                EmployeeName = existEmployee.FullName,
                Message = "Đã xóa thành công "
            };

            return response;
    
        }



    }
}
