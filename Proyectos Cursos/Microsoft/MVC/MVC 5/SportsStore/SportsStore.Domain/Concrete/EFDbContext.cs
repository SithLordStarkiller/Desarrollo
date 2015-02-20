using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Collections.Generic;
namespace SportsStore.Domain.Concrete 
{
        public class EFDbContext : DbContext
        {
            public DbSet<Product> Products { get; set; }
        }
}
