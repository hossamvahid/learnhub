using System.ComponentModel.DataAnnotations;

namespace learnhub_be.Common.Models.RequestModels
{
    public class LoginModels
    {
        [EmailAddress]
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
