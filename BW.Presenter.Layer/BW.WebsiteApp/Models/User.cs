using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVC5DemoProject_1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }

    public class UsersDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}