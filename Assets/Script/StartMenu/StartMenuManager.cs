using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;

public class StartMenuManager : MonoBehaviour
{
    [Header("UI Canvas")]
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject settingMenu;
    [SerializeField] GameObject selectCharacterMenu;

    //----------OPTIONS-----------
    [Header("Settings Tab")]
    [SerializeField] GameObject GameplaySettingsTab;
    [SerializeField] GameObject SoundSettingsTab;

    [Header("Sound Tab")]
    [SerializeField] Slider bgmVolumeSlider;
    [SerializeField] Text bgmVolumeText;
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] Text sfxVolumeText;

    [SerializeField] SaveSettingsData settings;

    private void Awake()
    {
        settings = SaveManager.LoadSaveSettingsData();

        bgmVolumeSlider.value = settings.bgmVolume;
        sfxVolumeSlider.value = settings.sfxVolume;

        DisableNewGame();
    }

    public void OpenSettings()
    {
        startMenu.SetActive(false);
        settingMenu.SetActive(true);
        GameplaySettingsTab.SetActive(true);
        SoundSettingsTab.SetActive(false);
        bgmVolumeText.text = ((int)(settings.bgmVolume * 100)).ToString();
        sfxVolumeText.text = ((int)(settings.sfxVolume * 100)).ToString();
    }

    public void OpenStartMenu()
    {
        startMenu.SetActive(true);
        settingMenu.SetActive(false);
        selectCharacterMenu.SetActive(false);
        SaveManager.SaveSettings(settings);
    }

    public void OpenSelectCharacterMenu()
    {
        startMenu.SetActive(false);
        settingMenu.SetActive(false);
        selectCharacterMenu.SetActive(true);
    }

    public void ChangeToGameplayTab()
    {
        GameplaySettingsTab.SetActive(true);
        SoundSettingsTab.SetActive(false);
    }

    public void ChangeToSoundTab()
    {
        GameplaySettingsTab.SetActive(false);
        SoundSettingsTab.SetActive(true);
    }

    public void DisableNewGame()
    {
        
    }

    public void SetBGMVolume(float newValue)
    {
        settings.bgmVolume = (float) decimal.Round((decimal) newValue, 2);
        bgmVolumeText.text = ((int)(settings.bgmVolume * 100)).ToString();
    }

    public void SetSFXVolume(float newValue)
    {
        settings.sfxVolume = (float) decimal.Round((decimal) newValue, 2);
        sfxVolumeText.text = ((int)(settings.sfxVolume * 100)).ToString();
    }
}
