using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    private Dictionary<StatusEffectID, Sprite> statusEffectSpriteDict;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        Sprite poisonSE = LoadSpriteIcon("Poison");
        Sprite stunSE = LoadSpriteIcon("Stun");
        Sprite bleedingSE = LoadSpriteIcon("Bleeding");
        Sprite weakeningSE = LoadSpriteIcon("Weakening");

        statusEffectSpriteDict = new Dictionary<StatusEffectID, Sprite>
        {
            {
                StatusEffectID.poison,
                poisonSE
            },
            {
                StatusEffectID.bleeding,
                bleedingSE
            },
            {
                StatusEffectID.stun,
                stunSE
            },
            {
                StatusEffectID.weakening,
                weakeningSE
            },
            {
                StatusEffectID.randomDebuff,
                null
            }
        };
    }

    public Sprite GetStatusEffectSprite(StatusEffectID id)
    {
        if(statusEffectSpriteDict.ContainsKey(id))
        {
            return statusEffectSpriteDict[id];
        }
        Debug.LogError("status effect sprite isn't exist!");
        return null;
    }

    private Sprite LoadSpriteIcon(string statusEffectName)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Sprite/StatusEffect/{0}", statusEffectName);
        return Resources.Load<Sprite>(sb.ToString());
    }
}
