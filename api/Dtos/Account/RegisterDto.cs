using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string? UserName { set; get;}
        [Required]
        [EmailAddress]
        public string? Email { set; get; }
        [Required]
        public string? Password { set; get; }
    }
}