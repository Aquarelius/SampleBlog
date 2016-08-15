using System.Web.Configuration;

namespace SB.Helpers
{
    public static class SettingsHelper
    {
        public static int PageSize
        {
            get
            {
                var res = 10; //default values
                if (WebConfigurationManager.AppSettings["pageSize"] != null)
                {
                    int.TryParse(WebConfigurationManager.AppSettings["pageSize"], out res);
                }
                return res;
            }
        }

    }
}