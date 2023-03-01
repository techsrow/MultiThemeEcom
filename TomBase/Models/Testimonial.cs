using System;
using System.ComponentModel.DataAnnotations;

namespace BasePackageModule2.Models
{
    public class Testimonail
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }

       
        

       

        [Required]
        public string Content { get; set; }

        public string Video { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreatedDate { get; set; }

        public Testimonail()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
