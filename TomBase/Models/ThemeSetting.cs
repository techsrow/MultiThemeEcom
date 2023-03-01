using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.Models
{
    public class ThemeSetting
    {
        public int Id { get; set; }
        public string ThemeName { get; set; }
        public string ThemeCategory { get; set; }
        public bool IsActive { get; set; }

        public string SkinType { get; set; }
    }
}
