using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class CombatUnit
{
    [SerializeField] protected string _name;
    [SerializeField] protected int _health;
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _attack;
    [SerializeField] protected int _defence;
    [SerializeField] protected GameObject _model;

    protected Animator _animator;
    protected bool _attacked = false;

    private static readonly int Die1 = Animator.StringToHash("Die");
    private static readonly int GetHit = Animator.StringToHash("GetHit");

    public string Name => _name;
    public int Defence => _defence;
    public GameObject Model => _model;
    public bool Attacked => _attacked;

    public void SetAnimator(Animator animator)
    {
        _animator = animator;
    }
    
    public IEnumerator TakeHit(int damage, CombatUnit target)
    {
        _health -= damage;
        _animator.SetTrigger(GetHit);
        Debug.Log($"{target.Name} took {damage} damage");
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        if (_health <= 0)
        {
            Die();
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        }
    }
    
    private void Die()
    {
        CombatManager.Instance.RemoveUnit(this);
        _animator.SetTrigger(Die1);
    }

    public void NegateAttacked() => _attacked = false;
}


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