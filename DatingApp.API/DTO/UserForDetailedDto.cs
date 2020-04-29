using System;
using System.Collections.Generic;
using DatingApp.API.Models;
namespace DatingApp.API.DTO
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<PhotoForDetailedDto> Photos { get;set; }
    }
}