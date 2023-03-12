using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.Models
{
    public class Faq
    {
        public int Id { get; set; }

        [Display(Name =("Question"))]
        public string Qus { get; set; }

        [Display(Name = ("Answere"))]
        public string Ans { get; set; }
    }
}
