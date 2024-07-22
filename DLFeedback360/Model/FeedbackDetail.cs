using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedback360.Model
{
    public class FeedbackDetail
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int FeedbackCategory { get; set; }
        public string FeedbackProvider { get; set; }
        public DateTime LastDate { get; set; }
        public Boolean? IsActive { get; set; }
    }
}
