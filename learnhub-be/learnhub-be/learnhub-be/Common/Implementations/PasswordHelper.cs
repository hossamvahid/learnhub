using learnhub_be.Common.Interfaces;

namespace learnhub_be.Common.Implementations
{
    public class PasswordHelper : IPassowrdHelper
    {
        public static string EncryptPassword(string? password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string? password , string? hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
