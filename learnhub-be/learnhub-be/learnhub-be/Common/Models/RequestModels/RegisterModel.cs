using System.ComponentModel.DataAnnotations;

namespace learnhub_be.Common.Models.RequestModels
{
    public class RegisterModel
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}
