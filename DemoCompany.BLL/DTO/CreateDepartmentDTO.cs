using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCompany.BLL.DTO
{
   
    public class CreateDepartmentRequestDTO
    {
        /*
         Field là trường dữ liệu mà mình sẽ bảo mật cái đó, không set và không get
            private String departmentName;

            public string getDepartmentName(){
                return departmentName;
            }

            public void setDepartmentName(string value){
               this.deparmentName = value;
            }
  
         */

        // _departmentName 

        public string DepartmentName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }


    public class CreateDepartmentResponseDTO
    {
        public string DepartmentName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
    }
}
