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
    public class UserLoginQuery : IRequest<AuthResultDto>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, AuthResultDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtConfig _jwtConfig;
        public UserLoginQueryHandler(UserManager<User> userMgr, IMapper mapper, IOptionsMonitor<JwtConfig> jwtCfg)
        {
            _userManager = userMgr ?? throw new ArgumentNullException(nameof(userMgr));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jwtConfig = jwtCfg.CurrentValue ?? throw new ArgumentNullException(nameof(jwtCfg));
        }
        public async Task<AuthResultDto> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser == null)
                throw new Exception("No account registered on this email");

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, request.Password);

            if (!isCorrect)
                throw new Exception("Incorrect password");

            var jwtToken = _mapper.Map<User>(request).GenerateJwtToken(_jwtConfig);

            return new AuthResultDto()
            {
                Success = true,
                Token = jwtToken
            };
        }
    }
}
