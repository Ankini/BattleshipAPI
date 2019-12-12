using AutoMapper;
using Battleship.Services.Config;
using Xunit.Sdk;

namespace Battleship.Tests.Util
{
    public class ConfigureAutoMapperAttribute : BeforeAfterTestAttribute
    {
        static ConfigureAutoMapperAttribute()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            });
        }
    }

}
