using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTO
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="Password should be more than 4 charachters")]
        public string Password { get; set; }
    }
}