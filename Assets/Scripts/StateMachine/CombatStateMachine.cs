using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{

    private StateMachine _stateMachine;
    void Start()
    {
        _stateMachine = new StateMachine();
        
        var start = new StartCombat();
        var enemyTurn = new EnemyTurn();
        var partyTurn = new PartyTurn();
        var win = new Win();
        var lose = new Lose();
        
        _stateMachine.AddState(start);
        _stateMachine.AddState(enemyTurn);
        _stateMachine.AddState(partyTurn);
        _stateMachine.AddState(win);
        _stateMachine.AddState(lose);
        
        _stateMachine.AddTransition(start, enemyTurn,
            () => CombatManager.Instance.Initialized && CombatManager.Instance.NextUnit().GetType() == typeof(Enemy));
        _stateMachine.AddTransition(start, partyTurn,
            () => CombatManager.Instance.Initialized && CombatManager.Instance.NextUnit().GetType() == typeof(PartyMember));
        _stateMachine.AddTransition(enemyTurn, enemyTurn,
            () => CombatManager.Instance.ActiveUnit.Attacked && CombatManager.Instance.NextUnit().GetType() == typeof(Enemy));
        _stateMachine.AddTransition(enemyTurn, partyTurn,
            () => CombatManager.Instance.ActiveUnit.Attacked && CombatManager.Instance.NextUnit().GetType() == typeof(PartyMember));
        _stateMachine.AddTransition(partyTurn, partyTurn,
            () => CombatManager.Instance.ActiveUnit.Attacked && CombatManager.Instance.NextUnit().GetType() == typeof(PartyMember));
        _stateMachine.AddTransition(partyTurn, enemyTurn,
            () => CombatManager.Instance.ActiveUnit.Attacked && CombatManager.Instance.NextUnit().GetType() == typeof(Enemy));
        _stateMachine.AddTransition(enemyTurn, lose, () => CombatManager.Instance.Party.Count <= 0);
        _stateMachine.AddTransition(partyTurn, lose, () => CombatManager.Instance.Party.Count <= 0);
        _stateMachine.AddTransition(partyTurn, win, () => CombatManager.Instance.Enemies.Count <= 0);
        _stateMachine.AddTransition(enemyTurn, win, () => CombatManager.Instance.Enemies.Count <= 0);
        
        _stateMachine.SetState(start);
    }

    void Update()
    {
        _stateMachine.Tick();
    }
}