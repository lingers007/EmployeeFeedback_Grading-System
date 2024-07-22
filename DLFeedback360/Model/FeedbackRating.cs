using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedback360.Model
{
    [Table("FeedbackRatings")]
    public class FeedbackRating
    {
        [Key]
        public int FRID { get; set; }
        public string ToID { get; set; }
        public string ByID { get; set; }
        public int QID { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedBy { get; set; }
    }
}
