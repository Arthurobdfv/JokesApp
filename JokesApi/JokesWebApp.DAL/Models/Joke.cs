using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokesWebApp.DAL.Models
{
    [Table("Joke")]
    public class Joke
    {
        public Guid JokeId { get; set; }
        [Required(ErrorMessage = "Question Is Required")]
        public string Question { get; set; }
        public string Answer { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }     
        [Required]
        public User Owner { get; set; }
        [ForeignKey(nameof(User))]
        public Guid OwnerId { get; set; }
        
    }
}
