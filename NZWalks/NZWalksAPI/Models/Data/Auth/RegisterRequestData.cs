using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Data.Auth
{
    public class RegisterRequestData
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        public required string[] Roles { get; set; }
    }
}
