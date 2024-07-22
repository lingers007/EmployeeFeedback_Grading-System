using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedback360.Model
{
    public  class Designation
    {
        [Key]
        public int DesignID { get; set; }

        public string Desig {  get; set; }  
    }
}
