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
        
        _stateMachine.SetState(start);
    }

    void Update()
    {
        _stateMachine.Tick();
    }
}

public class Lose: IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }
}

public class Win : IState
{
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }
}

public class PartyTurn: IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }
}

public class EnemyTurn : IState
{
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        var activeEnemy = (Enemy)CombatManager.Instance.ActiveUnit;
        activeEnemy.Attack();
    }

    public void OnExit()
    {
        CombatManager.Instance.NextTurn();
    }
}