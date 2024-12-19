using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model.Request
{
    public class CreateEmployeeRequest
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public int DepartmentID { get; set; }
        public string? Position { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
