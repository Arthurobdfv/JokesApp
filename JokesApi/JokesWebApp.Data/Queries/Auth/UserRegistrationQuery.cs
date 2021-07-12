using AutoMapper;
using JokesWebApi.Configuration;
using JokesWebApi.Extensions;
using JokesWebApp.DAL.Models;
using JokesWebApp.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JokesWebApp.Data.Queries.Auth
{
    public class UserRegistrationQuery : IRequest<AuthResultDto>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }    
    }

    public class UserRegistrationQueryHandler : IRequestHandler<UserRegistrationQuery, AuthResultDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtConfig _jwtConfig;
        public UserRegistrationQueryHandler(UserManager<User> userMgr, IMapper mapper, IOptionsMonitor<JwtConfig> jwtCfg)
        {
            _userManager = userMgr ?? throw new ArgumentNullException(nameof(userMgr));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jwtConfig = jwtCfg.CurrentValue ?? throw new ArgumentNullException(nameof(jwtCfg));
        }
        public async Task<AuthResultDto> Handle(UserRegistrationQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                throw new Exception("Email already being used for another account");

            var newUser = _mapper.Map<User>(request);
            var isCreated = await _userManager.CreateAsync(newUser, request.Password);
            if (isCreated.Succeeded)
            {
                var jwtToken = newUser.GenerateJwtToken(_jwtConfig);
                return new AuthResultDto()
                {
                    Success = true,
                    Token = jwtToken
                };
            } else
            {
                throw new Exception("Unable to create user");
            }
            return null;
        }
    }
}
