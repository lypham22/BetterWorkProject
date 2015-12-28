using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BW.Website.Resource;

namespace BW.Data.Contract.DTOs
{
   public class UserPasswordView
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validComaprePass")]
        public string ConfirmPassword { get; set; }
    }
}
