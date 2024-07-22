using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedback360.Model
{
    [Table("FeedbackSchedules")]
    public class FeedbackSchedule
    {
        [Key]
        public int Id { get; set; }
        public string Employee  { get; set; }
        public int FeedbackCategory { get; set; }
        public string FeedbackProvider { get; set; }
        public  DateTime LastDate{ get; set; }
        public Boolean ? IsActive {  get; set; }
    }
}
