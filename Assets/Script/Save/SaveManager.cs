using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Text;
using Newtonsoft.Json;

public static class SaveManager
{
    public static bool SaveGame(SaveGameData saveData)
    {
        if(!Directory.Exists(GetSaveFolderPath()))
        {
            Directory.CreateDirectory(GetSaveFolderPath());
        }

        try
        {
            string rawData = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            
            FileStream saveFile = File.Open(GetSaveGameFilePath(), FileMode.OpenOrCreate);
            saveFile.Write(Encoding.UTF8.GetBytes(rawData), 0, rawData.Length);
        }
        catch
        {
            Debug.LogError("Failed to save game.");
            return false;
        }
        return true;
    }

    public static bool SaveSettings(SaveSettingsData settingsData)
    {
        if(!Directory.Exists(GetSaveFolderPath()))
        {
            Directory.CreateDirectory(GetSaveFolderPath());
        }

        try
        {
            string rawData = JsonConvert.SerializeObject(settingsData, Formatting.Indented);
            
            FileStream saveFile = File.Open(GetSaveSettingsFilePath(), FileMode.OpenOrCreate);
            saveFile.Write(Encoding.UTF8.GetBytes(rawData), 0, rawData.Length);
        }
        catch
        {
            Debug.LogError("Failed to save settings.");
            return false;
        }
        return true;
    }

    public static SaveGameData LoadSaveGameData()
    {
        try
        {
            FileStream loadedFile = File.OpenRead(GetSaveGameFilePath());
            StreamReader reader = new StreamReader(loadedFile);

            string rawData = reader.ReadToEnd();
            SaveGameData loadedData = JsonConvert.DeserializeObject<SaveGameData>(rawData);
            return loadedData;
        }
        catch
        {
            Debug.LogError("Failed to load game");
            return null;
        }
    }

    public static SaveSettingsData LoadSaveSettingsData()
    {
        try
        {
            FileStream loadedFile = File.OpenRead(GetSaveSettingsFilePath());
            StreamReader reader = new StreamReader(loadedFile);

            string rawData = reader.ReadToEnd();
            SaveSettingsData loadedData = JsonConvert.DeserializeObject<SaveSettingsData>(rawData);
            return loadedData;
        }
        catch
        {
            Debug.LogError("Failed to load settings, create default settings");
            SaveSettings(CreateDefaultSettings());
            return null;
        }
    }

    /// <summary>
    /// this method is called when you press "NEW GAME"
    /// </summary>
    /// <returns>default save game data, 바로 사용할 수 있음</returns>
    private static SaveGameData CreateInitialSaveGame(List<CharacterSaveData> _characters)
    {
        SaveGameData save = new SaveGameData();

        save.version = 0.1f;
        save.seed = (int) System.DateTime.Today.Ticks;

        save.gold = 0;
        save.characters = _characters;
        // public List<int> deck;

        save.dungeonIndex = 1;

        return save;
    }

    private static SaveSettingsData CreateDefaultSettings()
    {
        SaveSettingsData defaultSettings = new SaveSettingsData();

        defaultSettings.bgmLoudness = 0.5f;
        defaultSettings.sfxLoudness = 0.5f;

        return defaultSettings;
    }

    private static string GetSaveGameFilePath()
    {
        string result = string.Format("{0}{1}", GetSaveFolderPath(), "SaveGameData.json");
        return result;
    }

    private static string GetSaveSettingsFilePath()
    {
        string result = string.Format("{0}{1}", GetSaveFolderPath(), "SaveSettingsData.json");
        return result;
    }

    private static string GetSaveFolderPath()
    {
        string result = string.Format("{0}{1}", Application.dataPath, "/Resources/Save/");
        return result;
    }
}
