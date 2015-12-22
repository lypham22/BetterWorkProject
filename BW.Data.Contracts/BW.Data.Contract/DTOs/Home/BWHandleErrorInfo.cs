using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Data.Contract.DTOs
{
    public class BWHandleErrorInfo
    {
        public Exception InnerException { get; set; }
        public string FriendlyErrorMessage { get; set; }
        public string StackTraceMessage { get; set; }
    }
}
