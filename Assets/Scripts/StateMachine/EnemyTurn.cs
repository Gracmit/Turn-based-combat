public class EnemyTurn : IState
{
    private Enemy _activeEnemy;
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        _activeEnemy = (Enemy)CombatManager.Instance.ActiveUnit;
        _activeEnemy.Attack();
    }

    public void OnExit()
    {
        _activeEnemy.NegateAttacked();
        CombatManager.Instance.NextTurn();
    }
}