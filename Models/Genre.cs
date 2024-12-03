using System.ComponentModel.DataAnnotations;

namespace MovieList.Models
{
    public class Genre
    {
        public string GenreId { get; set; }

        [Required(ErrorMessage = "Genre name is required.")]
        [StringLength(50, ErrorMessage = "Genre name can't be longer than 50 characters.")]
        public string Name { get; set; }
    }
}
