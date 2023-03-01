using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.Models
{
    public class Media
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Slug { get; set; }
       

        public string Image { get; set; }

       

        
        public string Date { get; set; }

       

    }
}
