using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedback360.Model
{
    [Table("RatingCriteria")]
    public class RatingCriteria
    {
        [Key]
        public int QId { get; set; }
        public decimal Rating { get; set; }
        public int DesignationID { get; set; }
    }
}
