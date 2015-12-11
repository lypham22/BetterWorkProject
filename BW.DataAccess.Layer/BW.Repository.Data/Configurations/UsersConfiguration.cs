using BW.Data.Contract.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Repository.Data.Configurations
{
    class UsersConfiguration : EntityTypeConfiguration<User>
    {
        public UsersConfiguration()
        {
            ToTable("User");
            HasKey(c => c.Id).Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Name).IsRequired().HasMaxLength(50);
        }
    }
   
}
