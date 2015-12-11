using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace BW.WebsiteApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is required. Validate Server")]
        [EmailAddress(ErrorMessage = "Invalid email address. Validate Server")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required. Validate Server")]
        [StringLength(1000, ErrorMessage = "Password must have least 6 word. Validate Server", MinimumLength = 6)]
        public string Password { get; set; }
        //[Required(ErrorMessage = "RePassword is required. Validate Server")]
        //[Compare("Password", ErrorMessage = "Passwords don't match. Validate Server")]
        //public string RePassword { get; set; }
        [Required(ErrorMessage = "Name is required. Validate Server")]
        [StringLength(1000, ErrorMessage = "Name must have least 6 word. Validate Server", MinimumLength = 6)]
        public string Name { get; set; }
        public string Role { get; set; }
    }

    public class UsersDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}