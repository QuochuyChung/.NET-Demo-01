using DemoCompany.DAL.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCompany.DAL
{
    public class DepartmentRepository
    {
        private DemoCompanyContext _context;

        public DepartmentRepository(DemoCompanyContext context)
        {
            _context = context;
        }

        public Department? GetDepartmentById(int id)
        {
            Department? department = _context.Departments.FirstOrDefault(d => d.DepartmentId == id);

            if (department == null)
            {
                return null;
            }

            return department;
        }

        public bool CreateNewDepartment(Department department)
        {
            // mới nằm ở trên ram
            _context.Add(department);

            // Add, Update, Delete -> RAM

            // gọi trực tiếp xuống db
            _context.SaveChanges();

            return true;
        }
    }
}
