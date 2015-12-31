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

        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validPassRequire")]
        [StringLength(1000, ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validPassMinLenght", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validConfirmPassRequire")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validComaprePass")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
