namespace FEMR.Core
{
    public class BCryptPasswordEncryptor : IPasswordEncryptor
    {
        private readonly int _workFactor;

        public BCryptPasswordEncryptor(int workFactor)
        {
            _workFactor = workFactor;
        }

        public string HashPassword(string password)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, _workFactor);

            return hashedPassword;
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
