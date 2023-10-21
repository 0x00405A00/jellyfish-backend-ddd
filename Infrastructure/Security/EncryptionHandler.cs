using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Security
{
    public class EncryptionHandler : IEncryptionHandler, IDisposable
    {
        public int c_global_mode = 0;
        public bool c_global_cryptmode = false;
        string[] ciphers = { "CBC", "CFB", "CTS", "ECB", "OFB" };
        string[] cryptmodes = { "Encrypt", "Decrypt" };

        public EncryptionHandler()
        {

        }
        ~EncryptionHandler()
        {
            Dispose();
        }
        public virtual void Dispose()
        {

            GC.SuppressFinalize(this);
        }
        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    switch (c_global_mode)
                    {
                        case 0:
                            AES.Mode = CipherMode.CBC;
                            break;
                        case 1:
                            AES.Mode = CipherMode.CFB;
                            break;
                        case 2:
                            AES.Mode = CipherMode.CTS;
                            break;
                        case 3:
                            AES.Mode = CipherMode.ECB;
                            break;
                        case 4:
                            AES.Mode = CipherMode.OFB;
                            break;
                    }
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }
        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    switch (c_global_mode)
                    {
                        case 0:
                            AES.Mode = CipherMode.CBC;
                            break;
                        case 1:
                            AES.Mode = CipherMode.CFB;
                            break;
                        case 2:
                            AES.Mode = CipherMode.CTS;
                            break;
                        case 3:
                            AES.Mode = CipherMode.ECB;
                            break;
                        case 4:
                            AES.Mode = CipherMode.OFB;
                            break;
                    }
                    c_global_mode = 0;
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }
        public string EncryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            // Hash the password with SHA256
            passwordBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
            string result = Convert.ToBase64String(bytesEncrypted);
            return result;
        }
        public string DecryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);
            string result = Encoding.UTF8.GetString(bytesDecrypted);
            return result;
        }
        public string CreateRandomKey(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder responseValue = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                responseValue.Append(valid[rnd.Next(valid.Length)]);
            }
            return responseValue.ToString();
        }
        public string SHA256(string str)
        {
            string responseValue = null;
            using (SHA256 sHA256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(str);
                byte[] hashBytes = sHA256.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                responseValue = sb.ToString();
            }
            return responseValue;
        }
        public string MD5(string str)
        {
            string responseValue = null;
            using (MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(str);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                responseValue = sb.ToString();
            }
            return responseValue;
        }
        public async Task<string> MD5Async(string str)
        {
            string responseValue = null;
            using (MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(str);
                using (MemoryStream memoryStream = new MemoryStream(inputBytes))
                {
                    byte[] hashBytes = await md5.ComputeHashAsync(memoryStream);

                    // Convert the byte array to hexadecimal string
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("X2"));
                    }
                    responseValue = sb.ToString();
                }
            }
            return responseValue;
        }
        public async Task<string> MD5Async(Stream stream)
        {
            string responseValue = null;
            using (MD5 md5 = System.Security.Cryptography.MD5.Create())
            {

                byte[] hashBytes = await md5.ComputeHashAsync(stream);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                responseValue = sb.ToString();
            }
            return responseValue;
        }
    }
}
