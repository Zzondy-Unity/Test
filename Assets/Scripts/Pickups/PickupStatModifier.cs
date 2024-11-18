
using System;
using System.Collections.Generic;
using UnityEngine;

public class PickupStatModifiers : PickupItem
{
    [SerializeField] private List<CharacterStat> statsModifiers = new List<CharacterStat>();

    protected override void OnPickedUp(GameObject gameObject)
    {
        CharacterStatHandler statHandler = gameObject.GetComponent<CharacterStatHandler>();

        foreach(CharacterStat modifier in statsModifiers)
        {
            Debug.Log("스탯적용중");
            statHandler.AddStatModifier(modifier);
        }

        //최대 체력을 올리거나 체력을 회복하는 경우
        HealthSystem healthSystem = gameObject.GetComponent<HealthSystem>();
        healthSystem.ChangeHealth(0);
    }
}
