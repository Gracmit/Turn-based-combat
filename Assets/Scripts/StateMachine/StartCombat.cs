public class StartCombat : IState
{
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        CombatManager.Instance.InstantiateBattlefield();
    }

    public void OnExit()
    {
        CombatManager.Instance.NegateInitialized();
    }
}