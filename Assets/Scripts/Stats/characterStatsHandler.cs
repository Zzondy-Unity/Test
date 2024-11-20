using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    // 기본스탯, 추가스탯 계산해서 최종스탯 계산하기
    // 일단 기본 스탯만

    [SerializeField] private CharacterStat baseStat;
    public CharacterStat CurrentStat { get; private set; } = new();

    public List<CharacterStat> statModifiers = new List<CharacterStat>();

    private readonly float MinAttackDelay = 0.03f;
    private readonly float MinAttackPower = 0.5f;
    private readonly float MinAttackSize = 0.4f;
    private readonly float MinAttackSpeed = .1f;
    private readonly float MinSpeed = 0.8f;
    private readonly int MinMaxHealth = 5;

    private readonly Dictionary<StatsChangeType, Func<float, float, float>> OperationMethod = new Dictionary<StatsChangeType, Func<float, float, float>>
    {
        {StatsChangeType.Add, (current, change) => current + change },
        {StatsChangeType.Multiple, (current, change) => current * change },
        {StatsChangeType.Override, (current, change) => change }
    };

    private void Awake()
    {
        InitalizeStat();
    }

    private void InitalizeStat()
    {
        UpdateCharacterStat();
        InitAttackSO();
    }

    private void InitAttackSO()
    {
        if (baseStat.attackSO != null)
        {
            //기본스탯 초기화
            baseStat.attackSO = Instantiate(baseStat.attackSO);
            CurrentStat.attackSO = Instantiate(baseStat.attackSO);
        }
    }

    private void UpdateCharacterStat()
    {
        //baseStat은 Override
        ApplyStatModifier(baseStat);

        //base가 먼저 실행되고, Add Multiple Override 순서대로 실행된다.
        foreach (CharacterStat stat in statModifiers.OrderBy(x => x.statsChangeType))
        {
            ApplyStatModifier(stat);
        }
    }

    public void AddStatModifier(CharacterStat modifier)
    {
        statModifiers.Add(modifier);
        UpdateCharacterStat();
    }
    public void RemoveStatModifier(CharacterStat modifier)
    {
        statModifiers.Remove(modifier);
        UpdateCharacterStat();
    }

    private void ApplyStatModifier(CharacterStat modifier)  //abstract class characterstat?
    {
        //operation이 어떤 연산을 할 것인지 정의
        Func<float, float, float> operation = OperationMethod[modifier.statsChangeType];

        UpdateBasicStats(operation, modifier);
        UpdateAttackStats(operation, modifier);
        if (CurrentStat.attackSO is RangedAttackSO currentRanged && modifier.attackSO is RangedAttackSO newRanged)
        {
            UpdateRangedAttackStats(operation, currentRanged, newRanged);
        }
    }


    private void UpdateRangedAttackStats(Func<float, float, float> operation, RangedAttackSO currentRanged, RangedAttackSO newRanged)
    {
        currentRanged.multipleProjectilesAngle = operation(currentRanged.multipleProjectilesAngle, newRanged.multipleProjectilesAngle);
        currentRanged.spread = operation(currentRanged.spread, newRanged.spread);
        currentRanged.duration = operation(currentRanged.duration, newRanged.duration);
        currentRanged.numberOfProjectilesPerShot = Mathf.CeilToInt(operation(currentRanged.numberOfProjectilesPerShot, newRanged.numberOfProjectilesPerShot));
        currentRanged.projectileColor = UpdateColor(operation, currentRanged.projectileColor, newRanged.projectileColor);
    }

    private Color UpdateColor(Func<float, float, float> operation, Color current, Color modifier)
    {
        return new Color(
                operation(current.r, modifier.r),
                operation(current.g, modifier.g),
                operation(current.b, modifier.b),
                operation(current.a, modifier.a));
    }

    private void UpdateAttackStats(Func<float, float, float> operation, CharacterStat modifier)
    {
        if (CurrentStat.attackSO == null || modifier.attackSO == null) return;

        var currentAttack = CurrentStat.attackSO;
        var newAttack = modifier.attackSO;

        // 변경을 적용하되, 최소값을 적용합니다.
        currentAttack.delay = Mathf.Max(operation(currentAttack.delay, newAttack.delay), MinAttackDelay);
        currentAttack.power = Mathf.Max(operation(currentAttack.power, newAttack.power), MinAttackPower);
        currentAttack.size = Mathf.Max(operation(currentAttack.size, newAttack.size), MinAttackSize);
        currentAttack.speed = Mathf.Max(operation(currentAttack.speed, newAttack.speed), MinAttackSpeed);
    }

    private void UpdateBasicStats(Func<float, float, float> operation, CharacterStat modifier)
    {
        CurrentStat.maxHealth = Mathf.Max((int)operation(CurrentStat.maxHealth, modifier.maxHealth), MinMaxHealth);
        CurrentStat.speed = Mathf.Max((int)operation(CurrentStat.speed, modifier.speed), MinSpeed);
    }
}