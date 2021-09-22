using System;
using System.IO;
using UnityEngine;

namespace Infrastructure.Database
{
    public static class Database
    {
        #region Consts

        private const string PLAYER_PREFS_KEY = "player_data";
        private const string DEFAULT_JSON =
            "{\"_lives\":3,\"_image\":\"\",\"_name\":\"Syymon\",\"_health\":100.0,\"_lastRivalIndex\":-1,\"_score\":0,\"_bestSucore\":0}";

        #endregion

        #region Consts

        #endregion

        #region Functions

        public static void SaveData()
        {
            string playerData = JsonUtility.ToJson(PlayerData.Instance);
            PlayerPrefs.SetString(PLAYER_PREFS_KEY, playerData);
        }

        public static void LoadData()
        {
            if (!PlayerPrefs.HasKey(PLAYER_PREFS_KEY))
            {
                var defaultPlayerDataJson = JsonUtility.ToJson(DEFAULT_JSON);
                PlayerPrefs.SetString(PLAYER_PREFS_KEY, defaultPlayerDataJson);
            }
            
            var playerData = PlayerPrefs.GetString(PLAYER_PREFS_KEY);
            var data = JsonUtility.FromJson<PlayerData>(playerData);
            PlayerData.Instance.Set(data);
        }

        #endregion
    }
}