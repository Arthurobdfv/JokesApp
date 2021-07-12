using AutoMapper;
using JokesWebApp.DAL.Models;
using JokesWebApp.Data.Queries.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokesWebApp.Data.Mapper.Profiles
{
    class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<UserRegistrationQuery, User>();
            CreateMap<UserLoginQuery, User>();
        }
    }
}
