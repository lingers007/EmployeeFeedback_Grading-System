using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedback360.Model
{
    [Table("UserDetails")]
   public class UserDetail
    {
        [Key]
        public int Id { get; set; } 
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Int64 MobNumber { get; set; }
        public string EmailID { get; set; }
        public string EmpID { get; set; }
        public string RoleID { get; set; }
        public string DesignID { get; set; }
        public DateTime? LastLoggedDate { get; set; }
        public string? CreatedBy { get; set; }

    }
}
