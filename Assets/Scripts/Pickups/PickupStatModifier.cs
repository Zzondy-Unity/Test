
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
            Debug.Log("����������");
            statHandler.AddStatModifier(modifier);
        }

        //�ִ� ü���� �ø��ų� ü���� ȸ���ϴ� ���
        HealthSystem healthSystem = gameObject.GetComponent<HealthSystem>();
        healthSystem.ChangeHealth(0);
    }
}
