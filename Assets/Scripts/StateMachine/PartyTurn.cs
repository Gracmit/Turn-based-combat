public class PartyTurn: IState
{
    private PartyMember _activeUnit;
    public void Tick()
    {
    }

    public void OnEnter()
    {
        _activeUnit = (PartyMember)CombatManager.Instance.ActiveUnit;
        _activeUnit.Attack();
    }

    public void OnExit()
    {
        _activeUnit.NegateAttacked();
        CombatManager.Instance.NextTurn();
    }
}