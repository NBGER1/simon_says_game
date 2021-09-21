using System;
using System.IO;
using UnityEngine;

namespace Gameplay.Scoreboard
{
    public static class Scoreboard
    {
        #region Consts

        private const string ENTRIES_PATH = @"Assets/Scoreboard/Entries/";

        #endregion

        #region Methods

        public static void AddNewEntry(ScoreboardEntryParams scoreboardEntryParams)
        {
            var fileName = "Entry_" + DateTime.Now.Ticks;
            var path = ENTRIES_PATH + fileName + ".json";
            string jsonObj = JsonUtility.ToJson(scoreboardEntryParams);
            Debug.Log($"{jsonObj} JSON OBJ");
            File.WriteAllText(path, jsonObj);
        }

        public static string[] GetEntryFileNames(int maxEntries)
        {
            return Directory.GetFiles(ENTRIES_PATH);
        }

        public static ScoreboardEntryParams GetEntryParams(string entryFileName)
        {
            var fileName = entryFileName;
            var path = ENTRIES_PATH + fileName + ".json";
            var jsonContent = File.ReadAllText(path);
            return JsonUtility.FromJson<ScoreboardEntryParams>(jsonContent);
        }

        #endregion
    }
}