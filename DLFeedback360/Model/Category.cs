using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedback360.Model
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }     
        public String CatDesc { get; set; }

        public DateTime? CreatedDtae { get; set; }
    }
}
