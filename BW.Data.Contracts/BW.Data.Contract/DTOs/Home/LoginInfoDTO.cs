
using BW.Website.Resource;
using System;
using System.ComponentModel.DataAnnotations;
namespace BW.Data.Contract.DTOs
{
    public class LoginInfoDTO
    {
        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validRequire")]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validRequire")]
        public string Password { get; set; }
    }
}
