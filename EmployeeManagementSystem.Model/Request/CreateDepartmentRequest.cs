using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model.Request
{
    public class CreateDepartmentRequest
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int? ManagerID { get; set; }
        public decimal? Budget { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
