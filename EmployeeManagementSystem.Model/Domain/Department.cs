using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model.Domain
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int? ManagerID { get; set; }
        public decimal? Budget { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        // Navigation Property
        public Employee Manager { get; set; }
        public ICollection<Employee> Employee { get; set; }
    }
}
