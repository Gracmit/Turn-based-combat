using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PartyMember : CombatUnit
{
    public void Attack() => CoroutineHandler.Instance.HandleCoroutine(HandleAttack());
    private static readonly int Attack1 = Animator.StringToHash("Attack");

    public IEnumerator HandleAttack()
    {
        var target = ChooseTarget();
        var damage = CountDamage(target.Defence);
        _animator.SetTrigger(Attack1);
        yield return new WaitForSeconds(.5f);
        yield return CoroutineHandler.Instance.HandleCoroutine(target.TakeHit(damage, target));
        _attacked = true;
    }

    private int CountDamage(int targetDefence)
    {
        var damage = 2 * (_attack) ^ 2 / (_attack + targetDefence);
        if (damage <= 0)
            damage = 1;
        return damage;
    }

    private Enemy ChooseTarget()
    {
        var enemies = CombatManager.Instance.Enemies;
        var index = Random.Range(0, enemies.Count);
        return (Enemy) enemies[index];
    }
}