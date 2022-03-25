using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private Transform[] _enemySpawnPoints;
    [SerializeField] private Transform[] _partySpawnPoints;
    
    private static CombatManager _instance;
    private List<CombatUnit> _turnQueue = new();
    private int _turnIndex = 0;
    private CombatUnit _activeUnit;
    private bool _initialized;

    public static CombatManager Instance => _instance;
    public CombatUnit ActiveUnit => _activeUnit;
    public bool Initialized => _initialized;
    
    public List<CombatUnit> Party => Enumerable.ToList(_turnQueue.Where(x => x.GetType() == typeof(PartyMember)));
    public List<CombatUnit> Enemies => Enumerable.ToList(_turnQueue.Where(x => x.GetType() == typeof(Enemy)));

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
        _activeUnit = _turnQueue[_turnIndex];
        _initialized = true;
    }

    private void InstantiateParty(List<PartyMember> party)
    {
        var spawnPointCounter = 0;
        foreach (var partyMember in party)
        {
            var model = Instantiate(partyMember.Model, _partySpawnPoints[spawnPointCounter]);
            partyMember.SetAnimator(model.GetComponent<Animator>());
            spawnPointCounter++;
            _turnQueue.Add(partyMember);
        }
    }

    private void InstantiateEnemies(List<Enemy> enemies)
    {
        var spawnPointCounter = 0;
        foreach (var enemy in enemies)
        {
            var model = Instantiate(enemy.Model, _enemySpawnPoints[spawnPointCounter]);
            enemy.SetAnimator(model.GetComponent<Animator>());
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

    public void NextTurn()
    {
        if (_turnIndex < _turnQueue.Count - 1)
        {
            _turnIndex++;
            _activeUnit = _turnQueue[_turnIndex];
        }
        else
        {
            _turnIndex = 0;
            _activeUnit = _turnQueue[_turnIndex];
        }
    }

    public void RemoveUnit(CombatUnit combatUnit)
    {
        _turnQueue.Remove(combatUnit);
    }
}