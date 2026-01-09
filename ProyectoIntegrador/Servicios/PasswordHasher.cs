namespace ProyectoIntegrador.LogicaAplication.Servicios
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool Verify(string passwordPlano, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(passwordPlano, hash);
        }
    }

}
