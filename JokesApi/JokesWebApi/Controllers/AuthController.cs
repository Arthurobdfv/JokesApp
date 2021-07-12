using JokesWebApi.Configuration;
using JokesWebApp.DAL.Models;
using JokesWebApp.Data.Queries.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtConfig _jwtConfig;
        public AuthController(UserManager<User> userMgr,IOptionsMonitor<JwtConfig> jwtCfg,[FromServices]IMediator _mediatr) : base(_mediatr)
        {
            _userManager = userMgr ?? throw new ArgumentException(nameof(_userManager));
            _jwtConfig = jwtCfg.CurrentValue ?? throw new ArgumentException(nameof(_jwtConfig));
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]UserRegistrationQuery _query)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await QueryAsync(_query));
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to register");
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginQuery query)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await QueryAsync(query));
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to login");
            }
            return BadRequest();
        }


    }
}
