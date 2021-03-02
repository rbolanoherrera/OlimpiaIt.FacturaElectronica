using Olimpia.FacturaElectronica.App_Start;
using System.Web.Http;

namespace Olimpia.FacturaElectronica
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IoCConfiguration.Initialize(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}