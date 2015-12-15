﻿using System.Data.Entity;
using BW.Data.Contract.DTOs;
using BW.Repository.Data.Configurations;

namespace BW.Repository.Data
{
    public class BWDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public BWDataContext(string connStringName) :
            base(connStringName)
        {
        }

        public BWDataContext() : base("DefaultConnection") { }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        /// <summary>
        /// Override OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsersConfiguration());
        }
    }
}