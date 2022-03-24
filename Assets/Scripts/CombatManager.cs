using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private Transform[] _enemySpawnPoints;
    [SerializeField] private Transform[] _partySpawnPoints;
    
    private static CombatManager _instance;
    private List<CombatUnit> _turnQueue = new List<CombatUnit>();
    private int _turnIndex = 0;
    private bool _initialized = false;

    public static CombatManager Instance => _instance;
    public bool Initialized => _initialized;
    
    public void NegateInitialized() => _initialized = false;

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
        _initialized = true;
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

    public CombatUnit NextUnit()
    {
        if (_turnIndex < _turnQueue.Count - 1)
            return _turnQueue[_turnIndex + 1];

        return _turnQueue[0];
    }
}
