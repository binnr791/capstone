using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Text;
using Newtonsoft.Json;

public static class SaveManager
{
    public static bool SaveGame(SaveData saveData)
    {
        try
        {
            string rawData = JsonConvert.SerializeObject(saveData);
            
            FileStream saveFile = File.Open(GetSaveFilePath(), FileMode.OpenOrCreate);
            saveFile.Write(Encoding.UTF8.GetBytes(rawData), 0, rawData.Length);
        }
        catch
        {
            Debug.LogError("Failed to save game.");
            return false;
        }
        return true;
    }


    private static string GetSaveFilePath()
    {
        string result = string.Format("{0}{1}", Application.dataPath, "/Resources/Save/SaveData.json");
        return result;
    }
}
