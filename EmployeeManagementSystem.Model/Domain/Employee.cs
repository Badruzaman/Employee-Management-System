using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model.Domain
{
    public class Employee
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
        public Department Department { get; set; }
        public ICollection<PerformanceReview> PerformanceReview { get; set; }
    }
}
