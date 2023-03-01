using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BasePackageModule1.Models;

namespace BasePackageModule1.Data
{
    public class BasePackageModule1Context : DbContext
    {
        public BasePackageModule1Context (DbContextOptions<BasePackageModule1Context> options)
            : base(options)
        {
        }

        public DbSet<BasePackageModule1.Models.Seo> Seo { get; set; }
    }
}
