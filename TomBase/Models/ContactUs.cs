using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.Models
{
    public class ContactUs
    {

        public int Id { get; set; }
        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Mob. No")]
        public string Number{ get; set; }
        [DisplayName("Subject")]
        [Required]
        public string Subject { get; set; }
        [DisplayName("Message")]
        [Required]
        public string Message { get; set; }
    }

}
