using System;
using UnityEngine;

[Serializable]
public class CombatUnit
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _attack;
    [SerializeField] private int _defence;
    [SerializeField] private GameObject _combatAvatar;
}

public class Enemy : CombatUnit
{
    
}

public class PartyMember : CombatUnit
{

}