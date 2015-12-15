
namespace BW.Data.Contract.DTOViews
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //public int Id { get; set; }
        ////[Required(ErrorMessage = "Email is required. Validate Server")]
        ////[EmailAddress(ErrorMessage = "Invalid email address. Validate Server")]
        //public string Email { get; set; }
        ////[Required(ErrorMessage = "Password is required. Validate Server")]
        ////[StringLength(1000, ErrorMessage = "Password must have least 6 word. Validate Server", MinimumLength = 6)]
        //public string Password { get; set; }
        ////[Required(ErrorMessage = "RePassword is required. Validate Server")]
        ////[Compare("Password", ErrorMessage = "Passwords don't match. Validate Server")]
        ////public string RePassword { get; set; }
        ////[Required(ErrorMessage = "Name is required. Validate Server")]
        ////[StringLength(1000, ErrorMessage = "Name must have least 6 word. Validate Server", MinimumLength = 6)]
        //public string Name { get; set; }
        //public string Role { get; set; }

        //[Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validEmailRequire")]
        //[EmailAddress(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validEmailRegex")]
        //public string Email { get; set; }
        //[Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validPassRequire")]
        //[StringLength(1000, ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validPassMinLenght", MinimumLength = 6)]
        //public string Password { get; set; }
        ////[Required(ErrorMessage = "RePassword is required. Validate Server")]
        ////[Compare("Password", ErrorMessage = "Passwords don't match. Validate Server")]
        ////public string RePassword { get; set; }
        //[Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validNameRequire")]
        //[StringLength(1000, ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validNameMinLenght", MinimumLength = 6)]
        //public string Name { get; set; }
        //public string Role { get; set; }
    }
}
