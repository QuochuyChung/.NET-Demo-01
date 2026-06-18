using DemoCompany.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCompany.DAL
{
    public class EmployeeRepository : IDisposable
    {
        // CLASS GIAO TIẾP TRUNG GIAN GIỮA CODE VÀ DATABASE 
        // MÌNH SẼ PHẢI TỰ CODE ĐỂ CHO NÓ GIAO TIẾP
        private readonly DemoCompanyContext _context;


        /*
            Hiện tại _context đang là null
            Có 2 cách để cho context có bộ nhớ để xử lí 
            + Tự động đăng kí trong constructor và Dependency Injection tự khai báo cho nó luôn 
            => Chỉ xài được khi không đăng kí OnConfiguring bên trong DbContext mà đăng kí DbContext trong program
            + Nếu đã đăng kí bên file DbContext thông qua hàm OnConfiguring thì phải tự khai báo thủ công cho _context
         */

        public EmployeeRepository()
        {
            // Khai báo thủ công - mở connection
            _context = new DemoCompanyContext();
        }

        public void Dispose()
        {
            // đóng connection
            _context.Dispose();
        }

        /*
            Nhiệm vụ của EF Core 
            + Dùng để chuyển đổi từ những câu lệnh code sang câu lệnh sql rồi gửi xuống cơ sở dữ liệu để lấy dữ liệu
            + DbContext sẽ là trung gian để thực hiện những cái công việc đó
         */

        public List<Employee> GetEmployees()
        {
            List<Employee> result = new List<Employee>();

            result = _context.Employees.Include(e => e.Department).ToList();

         
            return result;
            /*
                1 số câu lệnh Linq đặc trưng là chốt sổ xuống database
                + ToList(), FirstOrDefault(), ToHashSet(), SingleOrDefault(),... 
             */
        }

        public bool CreateEmployee(Employee emp)
        {
            _context.Add(emp);

            // khi gọi add - update - delete thì nó đang nằm trên ram


            return true;
        }

        public bool DeleteEmployee(Employee emp)
        {
            Employee? existEmp = GetEmployeeById(emp.EmployeeId);

            if(existEmp != null)
            {
                _context.Remove(emp);
                return true;
            }


            return false;
        }

        public Employee? GetEmployeeById(int id)
        {
            Employee? emp = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);

            if(emp == null)
            {
                return null;
            }

            return emp;
        }


        public bool UpdateEmployee(Employee emp)
        {
            Employee? existEmp = GetEmployeeById(emp.EmployeeId);

            if(existEmp != null)
            {
                existEmp.FullName = emp.FullName;
                existEmp.Phone = emp.Phone;
                existEmp.Email = emp.Email;
                existEmp.Salary = emp.Salary;

                _context.Update(existEmp);

                return true;
            }

            return false;
        }

        public Employee? GetByCondition(Func<Employee, bool> predict)
        { 
            Employee? emp = _context.Employees.FirstOrDefault(predict);

            if (emp == null)
            {
                return null;
            }

            return emp;
        }


    }
}
 