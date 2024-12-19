using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Model.Domain
{
    public class PerformanceReview
    {
        public int ReviewID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime ReviewDate { get; set; }
        public Byte? ReviewScore { get; set; }
        public string ReviewNotes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        // Navigation Property
        public Employee Employee { get; set; }
    }
}
