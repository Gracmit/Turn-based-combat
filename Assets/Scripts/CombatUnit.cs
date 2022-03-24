using System;
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
    private static readonly int Die1 = Animator.StringToHash("Die");
    private static readonly int GetHit = Animator.StringToHash("GetHit");

    public string Name => _name;
    public int Defence => _defence;
    public GameObject Model => _model;

    public void SetAnimator(Animator animator)
    {
        _animator = animator;
    }
    
    public void TakeHit(int damage, CombatUnit target)
    {
        _health -= damage;
        _animator.SetTrigger(GetHit);
        Debug.Log($"{target.Name} took {damage} damage");

        if (_health <= 0)
            Die();
    }
    
    private void Die()
    {
        CombatManager.Instance.RemoveUnit(this);
        _animator.SetTrigger(Die1);
    }
}


[Serializable]
public class Enemy : CombatUnit
{
    public void Attack()
    {
        var target = ChooseTarget();
        var damage = CountDamage(target.Defence);
        target.TakeHit(damage, target);
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
}