using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_relation.Models;
using Microsoft.EntityFrameworkCore;

namespace api_relation.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
            
        }

        public DbSet<Friend> friends { get; set; }
        public DbSet<Work> works { get; set; }
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Things> Things { get; set; }
    }
}