using DemoCompany.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCompany.DAL
{
    public class DepartmentRepository : IDisposable
    {
        private DemoCompanyContext _context;

        public DepartmentRepository()
        {
            _context = new DemoCompanyContext();    
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
