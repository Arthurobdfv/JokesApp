using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
//Remove After implementing CQRS
using JokesWebApp.DAL.Domain.Context;
using JokesWebApp.DAL.Models;
using JokesWebApp.DAL.Repositories.GenericRepository;
using Microsoft.Identity.Web.Resource;
using JokesWebApp.Data.Queries.Joke;
using MediatR;
using JokesWebApp.Data.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JokesWebApi.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class JokeController : BaseController
    {
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        public JokeController([FromServices]IMediator mediatr):base(mediatr)
        {

        }
        [HttpGet]
        [Route("GetJokes")]
        public async Task<ActionResult> Get([FromQuery]GetJokesQuery query)
        {
            return Ok(await QueryAsync(query));
        }
    }
}
