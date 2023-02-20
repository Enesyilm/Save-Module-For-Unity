using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using VaultSave.Saver;
using VaultSave.Systems;

namespace VaultSave.Commands
{ 
    public class VSLoadJsonToAesCommand:ILoader
    {
        public T Execute<T>(string path, VSSystemData vsSystemData)
        {
            byte[] encryptedData = File.ReadAllBytes(path);
            string dataAsJson = DecryptStringFromBytesAes(encryptedData, vsSystemData.Password);
            T objectToReturn = JsonConvert.DeserializeObject<T>(dataAsJson,new JsonSerializerSettings() { ObjectCreationHandling = ObjectCreationHandling.Replace });
            return objectToReturn;
        }

        private string DecryptStringFromBytesAes(byte[] cipherText, string password)
        {
            string plaintext = null;
            byte[] salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt);
                aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
                aes.IV = deriveBytes.GetBytes(aes.BlockSize / 8);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
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