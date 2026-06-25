using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCompany.BLL.DTO
{
    public class DeleteEmployeeRequestDTO
    {
        public int EmployeeID { get; set; }
    }

    public class DeleteEmployeeResponseDTO
    {
        public string EmployeeName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
