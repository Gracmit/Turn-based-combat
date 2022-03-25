using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Enemy : CombatUnit
{
    private static readonly int Attack1 = Animator.StringToHash("Attack");

    public void Attack() => CoroutineHandler.Instance.HandleCoroutine(HandleAttack());

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
        var damage = 2 * (_attack)^2 / (_attack + targetDefence);
        if (damage <= 0)
            damage = 1;
        return damage;
    }

    private PartyMember ChooseTarget()
    {
        var party = CombatManager.Instance.Party;
        var index= Random.Range(0, party.Count);
        return (PartyMember)party[index];
    }
}