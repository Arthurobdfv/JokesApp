using AutoMapper;
using JokesWebApp.DAL.Domain.Context;
using JokesWebApp.DAL.Repositories.GenericRepository;
using JokesWebApp.Data.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JokesWebApp.Data.Queries.Joke
{
    public class GetJokesQuery : IRequest<IEnumerable<JokeDto>>
    {

    }

    public class GetJokesQueryHandler : IRequestHandler<GetJokesQuery, IEnumerable<JokeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<DAL.Models.Joke, JokeContext> _jokeRepository;

        public GetJokesQueryHandler(IMapper mapper, IGenericRepository<DAL.Models.Joke, JokeContext> repo)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jokeRepository = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        public async Task<IEnumerable<JokeDto>> Handle(GetJokesQuery request, CancellationToken cancellationToken)
        {
            var jokes = new List<DAL.Models.Joke>();
            try
            {
                jokes = await _jokeRepository.GetAll().ToListAsync();
            }
            catch(Exception e)
            {

            }
            return _mapper.Map<IEnumerable<JokeDto>>(jokes);
        }
    }


}
