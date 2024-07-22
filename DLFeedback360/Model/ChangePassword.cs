using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedback360.Model
{
 public class ChangePassword
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoggedDate { get; set; }
    }
}
