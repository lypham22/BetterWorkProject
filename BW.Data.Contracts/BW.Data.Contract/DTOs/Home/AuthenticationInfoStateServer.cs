using System;
using System.Collections.Generic;

namespace BW.Data.Contract.DTOs
{
    [Serializable()]
    public class AuthenticationInfoStateServer
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<PermissionStateServer> Permissions { get; set; }
    }

    [Serializable()]
    public class PermissionStateServer
    {
        public string PermissionCode { get; set; }
    }
}