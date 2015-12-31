using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BW.Website.Resource;

namespace BW.Data.Contract.DTOs
{
    public class UserProfileView
    {
        public int UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validFirstNameRequire")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validLastNameRequire")]
        public string LastName { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
