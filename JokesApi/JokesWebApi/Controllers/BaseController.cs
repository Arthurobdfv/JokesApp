using JokesWebApp.Data.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        public BaseController([FromServices]IMediator mediator = null)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }

        public async Task<TResult> QueryAsync<TResult>(IRequest<TResult> req)
        {
                return await _mediator.Send(req);
        }

    }
}
