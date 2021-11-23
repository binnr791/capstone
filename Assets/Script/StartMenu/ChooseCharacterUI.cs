using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ChooseCharacterUI : MonoBehaviour
{
    [SerializeField] Text charName;
    [SerializeField] Text charDescription;

    public void UpdateCharText(string newName, string newDescription)
    {
        charName.text = newName;
        charDescription.text = newDescription;
    }
}
