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
    public class DepartmentService
    {
        // RIEENG VỀ FIELD PHẢI BẢO MẬT
        private readonly DepartmentRepository _departmentRepo;

        /*
         Service sẽ phải gọi sẵn repo đã được tương tác với db
            + Là nơi xử lí chính
            + Luồng xử lí chính của mình
                - RequestDTO -> Models/Entities (map trực tiếp với DB) -> ResponseDTO
         */

        public DepartmentService(DepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }


        public CreateDepartmentResponseDTO? CreateDepartment(CreateDepartmentRequestDTO request)
        {
            // validate dữ liệu
            if(string.IsNullOrEmpty(request.DepartmentName) || string.IsNullOrEmpty(request.Location))
            {
                return null;
            }

            // chuyển sang entites
            Department newDepartment = new Department()
            {
                DepartmentName = request.DepartmentName,
                Location = request.Location,
                Budget = 0,
                CreatedAt = DateTime.Now
            };


            // models/entities sẽ là trực tiếp lưu db
            _departmentRepo.CreateNewDepartment(newDepartment);


            // trả ra response dto
            CreateDepartmentResponseDTO response = new CreateDepartmentResponseDTO()
            {
                DepartmentName = newDepartment.DepartmentName,
                Location = newDepartment.Location,
                CreatedAt = newDepartment.CreatedAt
            };

            return response;
        }
        
    }
}
