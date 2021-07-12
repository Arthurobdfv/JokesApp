using AutoMapper;
using JokesWebApp.Data.Mapper.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokesWebApp.Data.Mapper
{
    public class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new JokeProfile());
            });
            return mapperConfig.CreateMapper();
        }
    }
}
