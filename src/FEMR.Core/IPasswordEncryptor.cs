namespace FEMR.Core
{
    public interface IPasswordEncryptor
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
