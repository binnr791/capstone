using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [Header("UI Canvas")]
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject settingMenu;

    [Header("Settings Tab")]
    [SerializeField] GameObject GameplaySettingsTab;
    [SerializeField] GameObject SoundSettingsTab;

    public void OpenSettings()
    {
        startMenu.SetActive(false);
        settingMenu.SetActive(true);
        GameplaySettingsTab.SetActive(true);
    }

    public void OpenStartMenu()
    {
        startMenu.SetActive(true);
        settingMenu.SetActive(false);
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
}
