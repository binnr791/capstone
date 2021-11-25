using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ChooseCharacterUI : MonoBehaviour
{
    [SerializeField] Text charName;
    [SerializeField] Text charDescription;
    [SerializeField] Image fullLenCharImage;

    [SerializeField] Image[] teamIcons;

    private readonly Color transparentColor = new Color(0f, 0f, 0f, 0f);

    public void UpdateCharInfo(string newName, string newDescription, Sprite newFullLenImage)
    {
        charName.text = newName;
        charDescription.text = newDescription;
        fullLenCharImage.sprite = newFullLenImage;
    }

    public void UpdateTeamIcon(int index, Sprite charIcon)
    {
        if(charIcon == null)
        {
            teamIcons[index].color = transparentColor;
        }
        else
        {
            teamIcons[index].color = Color.white;
        }

        teamIcons[index].sprite = charIcon;
    }
}
