using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace LMS_WEB_API_CF_12Dec.Models
{
    public class ApplyLeave
    {
        
        public int ApplyLeaveId { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Required")]
        public string NumberOfDays { get; set; }
        [Required(ErrorMessage = "Required")]
        //public int NumberOfDays { get; set; }
        public string LeaveType { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Status { get; set; }//prabi
        [Required(ErrorMessage = "Required")]
        public string LeaveReason { get; set; }//Earned Leave/Sick leave
                                               //public Employee Employees { get; set; }
        [Required(ErrorMessage = "Required")]
        public string EmployeeId { get; set; }//this is used for storing Employee Id
        [Required(ErrorMessage ="Required")]
        [DataType(DataType.Date)]
        public string AppliedOn { get; set; }//prabi

        [Required(ErrorMessage = "Required")]
        public string ManagerComments { get; set; }//prabi

    }
}
