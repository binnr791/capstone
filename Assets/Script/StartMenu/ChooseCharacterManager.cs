using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using Newtonsoft.Json;

public class ChooseCharacterManager : MonoBehaviour
{
    public static ChooseCharacterManager instance;

    [SerializeField] ChooseCharacterUI ui;

    private Dictionary<int, Image> iconImages;
    private Dictionary<int, Image> fullLenImages;
    [SerializeField] private CharacterChoiceData[] charDatas;

    [SerializeField] private CharacterChoiceData[] _selectedChars;

    public Button startButton;

    private CharacterChoiceData[] selectedChars
    {
        get {return _selectedChars;}
        set
        {
            UpdateTeam();
        }
    }

    private string charDataPath;

    private void Awake()
    {
        instance = this;

        charDataPath = Application.dataPath + "/Resources/Data/CharData.json";
        Debug.Log(charDataPath);

        iconImages = new Dictionary<int, Image>();
        fullLenImages = new Dictionary<int, Image>();

        string charData = File.ReadAllText(charDataPath);
        charDatas = JsonConvert.DeserializeObject<CharacterChoiceData[]>(charData);
    
        for(int i = 0; i < charDatas.Length; i++)
        {
            iconImages[i] = Resources.Load<Image>(charDatas[i].charIconImage);
            fullLenImages[i] = Resources.Load<Image>(charDatas[i].fullLengthCharImage);
        }

        _selectedChars = new CharacterChoiceData[3];

        UpdateTeam();
    }

    public (string, string) GetCharInfo(int id)
    {
        return (charDatas[id].name, charDatas[id].description);
    }

    public Image GetIconImage(int id)
    {
        return iconImages[id];
    }

    public Image GetFullLengthCharImage(int id)
    {
        return fullLenImages[id];
    }

    public void SelectCharacter(int index) // character select button, default id = 0
    {
        Debug.Log(index.ToString());
        bool deselected = DeselectCharacter(index, -1, index + 1);

        if(deselected)
        {
            Debug.Log("Deselect index : " + index.ToString());
            return;
        }

        for(int i = 0; i < selectedChars.Length; i++)
        {
            if(selectedChars[i].id == 0)
            {
                selectedChars[i] = charDatas[index + 1];
                return;
            }
        }
        // return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="selectIndex"></param>
    /// <param name="deselectIndex"></param>
    /// <param name="id"></param>
    /// <returns>return true when succeding deselecting</returns>
    public bool DeselectCharacter(int selectIndex, int deselectIndex, int id)
    {
        if(selectIndex < 0) // deselect button
        {
            if(deselectIndex >= 0)
            {
                Debug.Log("Deselect deselect index : " + deselectIndex.ToString());
                selectedChars[deselectIndex] = new CharacterChoiceData(); // set to default
                return true;
            }
        }
        else // select button
        {
            for(int i = 0; i < selectedChars.Length; i++)
            {
                if(selectedChars[i].id == id)
                {
                    Debug.Log("Deselect select index : " + selectIndex.ToString() + " id : " + id.ToString());
                    selectedChars[i] = new CharacterChoiceData();
                    return true;
                }
            }
        }
        return false;
    }

    public void UpdateTeam()
    {
        // check if user can start a game
        startButton.enabled = true;
        for(int i = 0; i < selectedChars.Length; i++)
        {
            if(selectedChars[i].id == 0)
            {
                startButton.enabled = false;
            }
        }

        // update image

    }

    public void UpdateCharText(int index)
    {
        ui.UpdateCharText(charDatas[index].name, charDatas[index].description);
    }
}

[System.Serializable]
public struct CharacterChoiceData
{
    public CharacterChoiceData(int id = 0, string name = "", string description = "",
    string charIconImage = "", string fullLengthCharImage = "")
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.charIconImage = charIconImage;
        this.fullLengthCharImage = fullLengthCharImage;
    }

    public int id;
    public string name;
    public string description;
    public string charIconImage;
    public string fullLengthCharImage;
}