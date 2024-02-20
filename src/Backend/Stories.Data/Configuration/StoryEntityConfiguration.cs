using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stories.Data.Models;

namespace Stories.Data.Configuration
{
    public class StoryEntityConfiguration : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.ToTable("Story");

            builder.Property(s => s.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(s => s.Title)
                   .IsRequired()
                   .HasMaxLength(80)
                   .IsUnicode(false);

            builder.Property(s => s.Description)
                   .IsRequired()
                   .HasMaxLength(300);
                   
            builder.Property(s => s.DepartmentId)
                   .IsRequired();
            builder.HasOne(s => s.Department)
                   .WithMany()
                   .HasForeignKey(s => s.DepartmentId)
                   .HasConstraintName("FK_Department_Story") ;      


                   
        }
    }
}