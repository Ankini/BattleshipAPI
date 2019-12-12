using AutoMapper;
using Battleship.Services.Config;
using System.Web.Http;

namespace Battleship.Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents(GlobalConfiguration.Configuration);
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            });
        }
    }
}
