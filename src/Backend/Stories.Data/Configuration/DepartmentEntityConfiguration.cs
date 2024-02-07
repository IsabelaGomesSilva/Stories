using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stories.Data.Models;

namespace Stories.Data.Configuration
{
    public class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
           builder.ToTable("Departament");
           
           builder.Property(d => d.Id)
                  .ValueGeneratedOnAdd();
        
           builder.Property(d => d.Name)
                  .IsRequired()
                  .HasMaxLength(80);

        }
    }
}