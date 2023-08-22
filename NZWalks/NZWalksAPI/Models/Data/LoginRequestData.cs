using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Data
{
    public class LoginRequestData
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
