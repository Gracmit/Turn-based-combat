using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private Transform[] _enemySpawnPoints;
    [SerializeField] private Transform[] _partySpawnPoints;
    
    private static CombatManager _instance;
    private List<CombatUnit> _turnQueue = new List<CombatUnit>();

    public static CombatManager Instance => _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void InstantiateBattlefield()
    {
        var gameManager = FindObjectOfType<GameManager>();
        InstantiateEnemies(gameManager.Enemies);
        InstantiateParty(gameManager.Party);

    }

    private void InstantiateParty(List<PartyMember> party)
    {
        var spawnPointCounter = 0;
        foreach (var partyMember in party)
        {
            Instantiate(partyMember.Model, _partySpawnPoints[spawnPointCounter]);
            spawnPointCounter++;
            _turnQueue.Add(partyMember);
        }
    }

    private void InstantiateEnemies(List<Enemy> enemies)
    {
        var spawnPointCounter = 0;
        foreach (var enemy in enemies)
        {
            Instantiate(enemy.Model, _enemySpawnPoints[spawnPointCounter]);
            spawnPointCounter++;
            _turnQueue.Add(enemy);
        }
    }
}
