using System;
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

        [Required]
        public string KnownAs { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime LastActive { get; set; }

        public UserForRegisterDto()
        {
           Created = DateTime.Now;
           LastActive = DateTime.Now; 
        }
        
    }
}