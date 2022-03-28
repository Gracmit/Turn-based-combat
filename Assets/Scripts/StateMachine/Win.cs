using UnityEngine;

public class Win : IState
{
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        Debug.Log("Win");
        var party = CombatManager.Instance.Party;
        foreach (var unit in party)
        {
            unit.Win();
        }
    }

    public void OnExit()
    {
    }
}