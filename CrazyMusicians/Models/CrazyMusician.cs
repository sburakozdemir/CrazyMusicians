using System.ComponentModel.DataAnnotations;

namespace CrazyMusicians.Models
{
    public class CrazyMusician
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Profession field is required.")]
        public string Profession { get; set; }

        [Required(ErrorMessage = "Fun Feature field is required.")]
        public string Funfeature { get; set; }
    }
}
