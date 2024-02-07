using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stories.Data.Models;

namespace Stories.Data.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.Property(u => u.Id)
                   .ValueGeneratedOnAdd();
            
            builder.Property(u => u.Name)
                   .HasMaxLength(100)
                   .IsRequired()
                   .IsUnicode(false);
                  
        }
    }
}