namespace HRM_API.Application.Helpers
{
    public class AuthorizationHelper
    {
        public static string GenerateSecurePassword(int length = 10)
        {
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*()-_=+<>?";
            const string allChars = upper + lower + digits + special;

            var random = new Random();
            var passwordChars = new char[length];

            // Garantizar al menos un carácter de cada tipo
            passwordChars[0] = upper[random.Next(upper.Length)];
            passwordChars[1] = lower[random.Next(lower.Length)];
            passwordChars[2] = digits[random.Next(digits.Length)];
            passwordChars[3] = special[random.Next(special.Length)];

            // Completar el resto con caracteres aleatorios
            for (int i = 4; i < length; i++)
                passwordChars[i] = allChars[random.Next(allChars.Length)];

            // Mezclar los caracteres
            return new string(passwordChars.OrderBy(_ => random.Next()).ToArray());
        }
    }
}
