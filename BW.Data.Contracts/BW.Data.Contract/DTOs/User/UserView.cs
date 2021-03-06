﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BW.Website.Resource;

namespace BW.Data.Contract.DTOs
{
    public class UserView
    {
        public UserView()
        {
            RoleDTOs = new List<RoleDTO>();
        }
        public string UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validFirstNameRequire")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validLastNameRequire")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }
        public List<RoleDTO> RoleDTOs { get; set; }






        // @"^[a-z0-9A-Z\s!.?\+\[\]_\-\*\/\=\p{L}]*$"
        //public int UserId { get; set; }

        //[Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validUsernameRequire")]
        //[StringLength(12, ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validUsernameLenght", MinimumLength = 6)]
        //[RegularExpression(@"^[a-z0-9A-Z\s.\[\]_\-\*\p{L}]*$", ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validUsernameRegex")]
        //public string UserName { get; set; }

        //[Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validEmailRequire")]
        //[EmailAddress(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validEmailRegex")]
        //public string Email { get; set; }

        //[Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validPassRequire")]
        //[StringLength(1000, ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validPassMinLenght", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validConfirmPassRequire")]
        //[Compare("Password", ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validComaprePass")]
        //[DataType(DataType.Password)]
        //public string ConfirmPassword { get; set; }
    }
}
