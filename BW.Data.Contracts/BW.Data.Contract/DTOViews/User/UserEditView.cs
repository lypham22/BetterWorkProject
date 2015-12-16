using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BW.Website.Resource;

namespace BW.Data.Contract.DTOViews
{
    public class UserEditView
    {
        // @"^[a-z0-9A-Z\s!.?\+\[\]_\-\*\/\=\p{L}]*$"
        public int UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validUsernameRequire")]
        [StringLength(12, ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validUsernameLenght", MinimumLength = 6)]
        [RegularExpression(@"^[a-z0-9A-Z\s.\[\]_\-\*\p{L}]*$", ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validUsernameRegex")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validEmailRequire")]
        [EmailAddress(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validEmailRegex")]
        public string Email { get; set; }
    }
}
