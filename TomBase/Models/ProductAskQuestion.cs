using BasePackageModule2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.Models
{
    public class ProductAskQuestion
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }




        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime QuestionPostedDate { get; set; } = DateTime.Now;
        public DateTime AnswerDate { get; set; }

    }
}
