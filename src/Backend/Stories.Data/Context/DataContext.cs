using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stories.Data.Configuration;
using Stories.Data.Models;

namespace Stories.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
         public DbSet<Department> Department {get; set;} 
         public DbSet<Story> Story {get; set;}
         public DbSet<User> User {get; set;}
         public DbSet<Vote> Vote {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.ApplyConfiguration( new DepartmentEntityConfiguration());
             modelBuilder.ApplyConfiguration( new StoryEntityConfiguration());
             modelBuilder.ApplyConfiguration( new UserEntityConfiguration());
             modelBuilder.ApplyConfiguration( new VoteEntityConfiguration());

            
        }


    }
}   