using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GistForms
{
    static class ConnectionHandler
    {

        public static readonly HttpClient httpClient;

        public static string clientID = Environment.GetEnvironmentVariable("GistAppClientID");
        public static string clientSecret = Environment.GetEnvironmentVariable("GistAppClientSecret");

        private static byte[] key;
        private static byte[] IV;

        private static string token;
        public static string Token { get { return token; } }
        public static string Scopes { get; set; }

        static ConnectionHandler()
        {
            httpClient = new HttpClient();
            var projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            key = File.ReadAllBytes(projectPath + @"\key.txt");
            IV = File.ReadAllBytes(projectPath + @"\IV.txt");
            httpClient.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            var userAgentName = "SomeUserAgentName";
            httpClient.DefaultRequestHeaders.Add("user-agent", userAgentName);
            TryGetToken();
        }

        private static void TryGetToken()
        {
            if (File.Exists("token"))
            {
                var decryptedToken = DecryptStringFromBytes_Aes(File.ReadAllBytes("token"), key, IV);
                if (!string.IsNullOrEmpty(decryptedToken))
                    AddTokenHeader(decryptedToken);
            }
        }
        public static void AddTokenHeader(string token)
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"token {token}");
            var encryptedToken = EncryptStringToBytes_Aes(token, key, IV);
            File.WriteAllBytes("token", encryptedToken);
            ConnectionHandler.token = token;
        }
        public static void RemoveTokenHeader()
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            token = "";
        }

        private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }
        private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}
