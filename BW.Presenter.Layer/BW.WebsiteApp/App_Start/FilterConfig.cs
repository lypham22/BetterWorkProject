using BW.Website.Common;
using System.Web.Http;
using System.Web.Mvc;

namespace BW.WebsiteApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new BWHandleError());
            filters.Add(new BWValidateAuthorization());
        }
    }
}