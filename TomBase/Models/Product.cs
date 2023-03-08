using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace BasePackageModule2.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [DisplayName("Main Image")]
        public string Image { get; set; }

        public ICollection<ProductImage> Images { get; set; }

        public string Description { get; set; }


        [DisplayName("Short Description")]
        [MaxLength(200)]
        public string ShortDescription { get; set; }



        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "Sub Category")]
        public int? SubCategoryId { get; set; }

        [DisplayName("Sub Category")]
        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }

        [DisplayName("Base Price")]
        public double? BasePrice { get; set; }

        [Required]
        [DisplayName("sku")]
        public string sku { get; set; }
        [DisplayName("Discount Amount")]
        public double? DiscountAmount { get; set; }


        [DisplayName("Product Tags")]
        public string ProductTags { get; set; }

        [DisplayName("Free Shipping")]
        public bool FreeShipping { get; set; }

        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }


        public string ShopPageHeeaderImage { get; set; }


        public bool Featured { get; set; }
        public bool Essential { get; set; }
        public bool hotselling { get; set; }
        public bool  premium { get; set; }
        public bool traditional { get; set; }
        public bool DealOfTheDay { get; set; }


        public string Slug  { get; set; }

        [DisplayName("Final Price")]
        public double? FinalPrice
        {
            get
            {
                if (DiscountAmount == null)
                {
                    if (BasePrice != null)
                    {
                        return Math.Round((double)BasePrice);
                    }

                    return null;
                }

                if (BasePrice != null)
                    return Math.Round((double)(BasePrice - DiscountAmount), 2);
                return BasePrice;
            }
        }
        [DisplayName("Discount Percentage")]
        public double? DiscountPercentage
        {
            get
            {
                if (DiscountAmount != null) return Math.Round((double)((DiscountAmount / BasePrice) * 100), 2);
                return 0;
            }
        }


        public double? RoundedBasePrice => Math.Round((double)BasePrice, 2);


        public string GenerateSlug()
        {
            string phrase = string.Format("{0}-{1}", Id, Name);

            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
