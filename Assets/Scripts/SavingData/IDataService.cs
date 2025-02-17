using System.IO;
using UnityEngine;

namespace CubeHopper.SavingData
{
    public interface IDataService
    {
        bool CheckPathExistence(string relativePath) 
        {
            return (File.Exists(Application.persistentDataPath + relativePath)) ;
        }
        bool SaveData<T>(string relativePath, T data, bool isEncrypted);
        T LoadData<T>(string relativePath, bool isEncrypted);
    }
}
