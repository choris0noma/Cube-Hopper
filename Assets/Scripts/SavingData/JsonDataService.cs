using CubeHopper.SavingData;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace CubeHopper
{
    public class JsonDataService : IDataService
    {
        private const string KEY = "+uQebOpyWpj77A4xujzLnllkG+XKNG4ttDy/7wrV70M=";
        private const string IV = "3RJDQVDDymRlCMz5VxnnLg==";
        public bool SaveData<T>(string relativePath, T data, bool isEncrypted)
        {
            string path = Application.persistentDataPath + relativePath;
            try 
            {
                if (File.Exists(path)) File.Delete(path);
                using FileStream stream = File.Create(path);
                if (isEncrypted) 
                { 
                    WriteEncryptedData(data, stream);
                }
                else 
                {
                    stream.Close();
                    File.WriteAllText(path, JsonConvert.SerializeObject(data));
                }
               
                return true;
            }
            catch(Exception e)
            {
                Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
                return false;
            }
        }
        private void WriteEncryptedData<T>(T data, FileStream stream)
        {
            using Aes aesProvider = Aes.Create();
            aesProvider.Key = Convert.FromBase64String(KEY);
            aesProvider.IV = Convert.FromBase64String(IV);
            using ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor();
            using CryptoStream cryptoStream = new CryptoStream 
                (stream, cryptoTransform, CryptoStreamMode.Write);

            
            cryptoStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data)));
        }
        public T LoadData<T>(string relativePath, bool isEncrypted)
        {
            string path = Application.persistentDataPath + relativePath;
            if (!File.Exists(path)) 
            {
                Debug.LogError("File doesn't exist");
                throw new FileNotFoundException($"");
            }
            try
            {
                T data;
                if (isEncrypted)
                {
                    data = ReadEncryptedData<T>(path);
                }
                else
                {
                    data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                }
                return data;
            }
            catch (Exception e) 
            {
                Debug.LogError($"Error while loading data: {e.Message} {e.StackTrace}");
                throw e;
            }
        }

        private T ReadEncryptedData<T>(string path)
        {
            byte[] fyleBytes = File.ReadAllBytes(path);
            using Aes aesProvider = Aes.Create();
            aesProvider.Key = Convert.FromBase64String(KEY);
            aesProvider.IV= Convert.FromBase64String(IV);
            using ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor
                (aesProvider.Key, aesProvider.IV);
            using MemoryStream stream = new MemoryStream(fyleBytes);
            using CryptoStream cryptoStream = new CryptoStream
                (stream, cryptoTransform, CryptoStreamMode.Read);
            using StreamReader reader = new StreamReader(cryptoStream);
            string result = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
