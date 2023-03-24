using System.ComponentModel.DataAnnotations;

namespace SalesSystem.Models.Request
{

    public class AuthReq
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
