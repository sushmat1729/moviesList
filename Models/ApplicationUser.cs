namespace MovieList.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public string AvatarUrl { get; set; }
        public bool IsPrivate { get; set; }
    }

}
