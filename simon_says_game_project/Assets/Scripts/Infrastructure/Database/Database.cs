using System.IO;
using UnityEngine;

namespace Infrastructure.Database
{
    public static class Database
    {
        #region Consts

        private const string LOCAL_DATA_PATH = "Assets/Resources/PlayerData.json";

        #endregion

        #region Fields

        #endregion

        #region Functions

        public static void SaveData()
        {
            var data = PlayerData.Instance;
            string jsonObj = JsonUtility.ToJson(data);
            File.WriteAllText(LOCAL_DATA_PATH, jsonObj);
        }

        public static void LoadData()
        {
            var jsonContent = File.ReadAllText(LOCAL_DATA_PATH);
            var data = JsonUtility.FromJson<PlayerData>(jsonContent);
            PlayerData.Instance.Set(data);
        }

        #endregion
    }
}