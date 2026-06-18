using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCompany.BLL.DTO
{
    public class CreateEmployeeRequestDTO
    {
        // FIELD SẼ LÀ NHỮNG THUỘC TÍNH MÀ MÌNH PHẢI GIẤU ĐI KHÔNG CHO NGƯỜI KHÁC BIẾT ĐƯỢC
        /*
         VD: private String fullName;
         */

        // BIẾN THÀNH 1 CÁI FIELD JSON 
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set;  } = string.Empty;

        public string? PhoneNumber { get; set; }
        public string? Location { get; set;  }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set;  }
        
    }

    public class CreateEmployeeResponseDTO
    {
        public string FullName { get; set;  } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? HireDate { get; set;  }
    }
}
