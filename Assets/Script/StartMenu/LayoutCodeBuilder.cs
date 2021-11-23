using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayoutCodeBuilder : MonoBehaviour
{
    public RectTransform charOptionLayout;
    public RectTransform teamLayout;

    private void Start()
    {
        for(int i = 0; i < charOptionLayout.childCount; i++)
        {
            Button btn = charOptionLayout.GetChild(i).GetComponent<Button>();
            if(btn != null)
            {
                int x = i; // dont delete, copying is needed
                btn.onClick.AddListener(() => ChooseCharacterManager.instance.SelectCharacter(x));
            }

            ChooseCharButton ccb = charOptionLayout.GetChild(i).GetComponent<ChooseCharButton>();
            if(ccb != null)
            {
                int x = i; // dont delete, copying is needed
                ccb.index = i+1; // zero is default data
            }

        }

        for(int i = 0; i < teamLayout.childCount; i++)
        {
            Button btn = teamLayout.GetChild(i).GetComponent<Button>();
            if(btn != null)
            {
                int x = i; // dont delete, copying is needed
                btn.onClick.AddListener(() => ChooseCharacterManager.instance.DeselectCharacter(-1, x, x+1));
            }
        }
    }
}
