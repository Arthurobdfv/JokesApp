using JokesWebApp.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokesWebApp.DAL.Domain.Context
{
    public class JokeContext : IdentityDbContext<User>
    {
        public JokeContext(DbContextOptions<JokeContext> options) : base (options)
        {

        }

        //public DbSet<User> Users { get; set; }

        public DbSet<Joke> Jokes { get; set; }
    }
}
