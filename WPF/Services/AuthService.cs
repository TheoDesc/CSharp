namespace WPF.Services
{
    public static class AuthService
    {
        private static readonly string MotDePasseAdmin = "admin";


        public static bool VerifierMotDePasse(string motDePasse)
        {
            return motDePasse == MotDePasseAdmin;
        }
    }
}