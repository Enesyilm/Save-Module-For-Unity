using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using VaultSave.SaveConfig;

public class JsonWithAES : MonoBehaviour
{
    

    public GameData gameData=new GameData();
    private readonly string password = "your_password_here";

    private void Start()
    {
        
        //Save(gameData);

       gameData=Load<GameData>();

       Debug.Log(gameData.ExampleIntData);
    }

    public void Save<T>(T data, string fileName = "gameData") where T : class
    {
        string dataAsJson = JsonConvert.SerializeObject(data);
        byte[] encryptedData = EncryptStringToBytesAes(dataAsJson, password);
        string filePath = Application.dataPath + "/" + fileName + ".dat";
        File.WriteAllBytes(filePath, encryptedData);
    }

    public T Load<T>(string fileName = "gameData") where T : class, new()
    {
        T data = default(T);
        string filePath = Application.dataPath + "/" + fileName + ".dat";
        if (File.Exists(filePath))
        {
            byte[] encryptedData = File.ReadAllBytes(filePath);
            string dataAsJson = DecryptStringFromBytesAes(encryptedData, password);
            data = JsonConvert.DeserializeObject<T>(dataAsJson);
        }
        return data;
    }

    private static byte[] EncryptStringToBytesAes(string plainText, string password)
    {
        byte[] encrypted;
        byte[] salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        using (Aes aes = Aes.Create())
        {
            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt);
            aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
            aes.IV = deriveBytes.GetBytes(aes.BlockSize / 8);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
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

    private static string DecryptStringFromBytesAes(byte[] cipherText, string password)
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