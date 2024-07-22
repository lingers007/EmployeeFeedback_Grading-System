using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedback360.Model
{
    [Table("AddQuestions")]
    public class AddQuestion
    {
        [Key]
        public int ID { get; set; }
        public int Qid { get; set; }

        public string QDescription { get; set; }
        public string DesignID { get; set; }

    }
}
