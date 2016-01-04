using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BW.Website.Resource;
using System.Web.Mvc;

namespace BW.Data.Contract.DTOs
{
    [MetadataType(typeof(UserCreateView))]  
    public class UserCreateView
    {
        public int UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validFirstNameRequire")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validLastNameRequire")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validEmailRequire")]
        [EmailAddress(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validEmailRegex")]
        [Remote("IsEmailExits", "User", ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validEmailIsExit")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validPassRequire")]
        [StringLength(1000, ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validPassMinLenght", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validConfirmPassRequire")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validComaprePass")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public string RoleId { get; set; }

        public List<RoleListView> roles { get; set; }
    }
}
