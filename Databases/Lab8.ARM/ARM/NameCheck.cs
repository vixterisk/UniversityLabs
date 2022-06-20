using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ARM
{
    static class NameCheck
    {
        public static readonly Regex loginRegex = new Regex(@"^[~`""'!@№#;$%:^&?*(){}\[\]\-_=+<>,./\\|ёа-яa-z0-9\s]+$", RegexOptions.IgnoreCase);
        public static string WhitespaceFormat(string loginText)
        {
            loginText = Regex.Replace(loginText, @"[\s]+", " ");
            return loginText.Trim(new char[] { ' ' });
        }
        public static string Formatted(string str)
        {
            return WhitespaceFormat(str).ToLower();
        }
        public static bool isLoginValid(string login)
        {
            return loginRegex.IsMatch(login);
        }
        public static bool LoginChangeCheck(string login, int id)
        {
            using (var users = new userModel())
            {
                foreach (var curUser in users.user)
                {
                    if (curUser.login.ToLower() == login.ToLower() && curUser.id != id) return false;
                }
            }
            return true;
        }
        public static bool NewLoginCheck(string login)
        {
            using (var users = new userModel())
            {
                foreach (var curUser in users.user)
                {
                    if (curUser.login.ToLower() == login.ToLower()) return false;
                }
            }
            return true;
        }
        static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        public static string GetPasswordHash(string password, string salt)
        {
            var saltedPassword = WhitespaceFormat(password) + salt;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string hash = GetHashString(saltedPassword);
                return hash;
            }
        }
    }
}
