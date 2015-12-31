using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BW.Website.Resource;

namespace BW.Data.Contract.DTOs
{
    public class UserProfileDTO
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
