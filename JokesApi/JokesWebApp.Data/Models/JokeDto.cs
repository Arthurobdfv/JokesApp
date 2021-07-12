using System;

namespace JokesWebApp.Data.DTO
{
    public class JokeDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
