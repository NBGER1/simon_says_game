using System;
using System.IO;
using UnityEngine;

namespace Infrastructure.Database
{
    public static class Database
    {
        #region Consts

        private const string LOCAL_DATA_PATH = "Assets/Resources/PlayerData.json";

        private const string DEFAULT_JSON =
            "{\"_lives\":3,\"_image\":\"\",\"_name\":\"Syymon\",\"_health\":100.0,\"_lastRivalIndex\":-1,\"_score\":0,\"_bestSucore\":0}";

        #endregion

        #region Consts

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
            if (jsonContent.Equals("") || jsonContent.Length < 1)
            {
                jsonContent = DEFAULT_JSON;
            }

            var data = JsonUtility.FromJson<PlayerData>(jsonContent);
            SaveData();
            PlayerData.Instance.Set(data);
        }

        #endregion
    }
}