namespace learnhub_be.Common.Interfaces
{
    public interface IPassowrdHelper
    {
        public abstract static string EncryptPassword(string? password);

        public abstract static bool VerifyPassword(string? password, string? hashedPassword);
    }
}
