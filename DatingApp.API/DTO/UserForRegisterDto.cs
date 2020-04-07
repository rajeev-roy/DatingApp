using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTO
{
    public class UserForRegisterDto
    {
        [Required]
        public string username { get; set; }

        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="Password should be more than 4 charachters")]
        public string password { get; set; }
    }
}