using AutoMapper;
using Battleship.BL.Common;
using Battleship.BL.Entities;
using Battleship.BL.Models;
using System.Web;

namespace Battleship.Services.Config
{
    /// <summary>
    /// Battleship - Automapper - Register
    /// </summary>
    public class MapperConfig:Profile
    {
        //public static void ConfigureMapping()
        public MapperConfig()
        {
            //    var config = new MapperConfiguration(cfg =>
            //      cfg.CreateMap<ShipPointLocationModel, ShipPoint>());
            //    HttpContext.Current.Application[GameConstant.MapperKey] = config;
            //    _mapper = config.CreateMapper();
            CreateMap<ShipPointLocationModel, ShipPoint>().ReverseMap();
        }
    }
}