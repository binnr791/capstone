using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    public Dictionary<int, effectFunc> idToEffect;
    public delegate void effectFunc();
    
    private void Awake()
    {
        idToEffect = new Dictionary<int, effectFunc>();

        idToEffect[0] = BloodSucking;
        idToEffect[1] = Defense;
        idToEffect[2] = SingleTargetAttack;
        idToEffect[3] = SingleTargetHeal;
        idToEffect[4] = AttackAllEnemy;

    }

    public effectFunc GetEffect(int id)
    {
        return idToEffect[id];
    }

    public void BloodSucking()
    {

    }

    public void Defense()
    {

    }

    public void SingleTargetAttack()
    {

    }

    public void SingleTargetHeal()
    {

    }

    public void AttackAllEnemy()
    {

    }
}
