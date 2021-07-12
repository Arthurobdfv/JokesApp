using AutoMapper;
using JokesWebApp.DAL.Models;
using JokesWebApp.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokesWebApp.Data.Mapper.Profiles
{
    public class JokeProfile : Profile
    {
        public JokeProfile()
        {
            CreateMap<Joke, JokeDto>();
        }
    }
}
