using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedback360.Model
{
    [Table("FeedbackQustions")]
    public class FeedbackQustion
    {
        [Key]
        public int QID { get; set; }
        public string QDescription { get; set; }
        public Boolean IsActive { get; set; }

        public int DesigID { get; set; }






    }
}