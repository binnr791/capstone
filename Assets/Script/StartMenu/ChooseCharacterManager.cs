using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using Newtonsoft.Json;

using game.util;

public class ChooseCharacterManager : MonoBehaviour
{
    public static ChooseCharacterManager instance;

    [SerializeField] ChooseCharacterUI ui;

    private Dictionary<int, Sprite> iconImages;
    private Dictionary<int, Sprite> fullLenImages;
    [SerializeField] private CharacterChoiceData[] charDatas;

    [SerializeField] private CharacterChoiceData[] _selectedChars;

    private const string charImagePath = "Sprite/Character/";

    [Header("Button UI")]
    public Button startButton;
    public Color enabledTextColor = new Color(0.1f, 0.1f, 0.1f);
    public Color disabledTextColor = new Color(0.85f, 0.85f, 0.85f);

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

        iconImages = new Dictionary<int, Sprite>();
        fullLenImages = new Dictionary<int, Sprite>();

        string charData = File.ReadAllText(charDataPath);
        charDatas = JsonConvert.DeserializeObject<CharacterChoiceData[]>(charData);
    
        for(int i = 0; i < charDatas.Length; i++)
        {
            iconImages[i] = Resources.LoadAll<Sprite>(StringMaker.Concatenate
                (charImagePath, charDatas[i].charIconImage))[0];
            fullLenImages[i] = Resources.LoadAll<Sprite>(StringMaker.Concatenate
                (charImagePath, charDatas[i].fullLengthCharImage))[0];
            
        }

        _selectedChars = new CharacterChoiceData[3];

        UpdateTeam();
    }

    public (string, string) GetCharInfo(int id)
    {
        return (charDatas[id].name, charDatas[id].description);
    }

    public Sprite GetIconImage(int id)
    {
        return iconImages[id];
    }

    public Sprite GetFullLengthCharImage(int id)
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
            UpdateTeam();
            return;
        }

        for(int i = 0; i < selectedChars.Length; i++)
        {
            if(selectedChars[i].id == 0)
            {
                selectedChars[i] = charDatas[index + 1];
                UpdateTeam();
                return;
            }
        }
        UpdateTeam();
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
                UpdateTeam();
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
                    UpdateTeam();
                    return true;
                }
            }
        }
        UpdateTeam();
        return false;
    }

    public void UpdateTeam()
    {
        // check if user can start a game
        startButton.enabled = true;
        startButton.transform.GetChild(0).GetComponent<Text>().color = enabledTextColor;
        Debug.Log("len : " + selectedChars.Length);
        for(int i = 0; i < selectedChars.Length; i++)
        {
            if(selectedChars[i].id == 0)
            {
                startButton.enabled = false;
                startButton.transform.GetChild(0).GetComponent<Text>().color = disabledTextColor;
                UpdateTeamIcon(i, null);
            }
            else
            {
                UpdateTeamIcon(i, iconImages[selectedChars[i].id]);
            }
        }
    
        // update image

    }

    public void UpdateCharInfo(int index)
    {
        ui.UpdateCharInfo(charDatas[index].name, charDatas[index].description, fullLenImages[index]);
    }
    public void UpdateTeamIcon(int index, Sprite icon)
    {
        ui.UpdateTeamIcon(index, icon);
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