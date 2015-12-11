using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Data.Contract.DTOs_View
{
  public  class UserViewModels
    {
        public string UserName { get; set; }
        public int Id { get; set; }
        public string UserAddress { get; set; }
    }

    public class UserInfo
    {
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}
