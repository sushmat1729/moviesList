using System;
using System.ComponentModel.DataAnnotations; //Using the directive

namespace MovieList.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set;}
        [Required(ErrorMessage = "Please enter a year.")]
        [Range(1889, 2999, ErrorMessage = "Year must be after 1889.")]
        public int? Year { get; set; }
        [Required(ErrorMessage = "Please enter a rating.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int? Rating { get; set; }
        public Genre Genre { get; set; }
        [Required(ErrorMessage = "Please enter a genre.")]
        public string GenreId { get; set; }

        // New Properties


       // [Required(ErrorMessage = "Please enter a director.")]
       // [StringLength(100, ErrorMessage = "Director's name cannot exceed 100 characters.")]
        public string Director { get; set; }

        [Range(1, 300, ErrorMessage = "Duration must be between 1 and 300 minutes.")]
        public int? Duration { get; set; }  // Duration in minutes
        public string Slug => Name?.Replace(' ', '-').ToLower() + '-' + Year?.ToString();
    }
}
