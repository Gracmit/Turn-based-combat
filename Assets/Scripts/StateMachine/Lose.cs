using UnityEngine;

public class Lose: IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        Debug.Log("Lose");
        var enemies = CombatManager.Instance.Enemies;
        foreach (var unit in enemies)
        {
            unit.Win();
        }
    }

    public void OnExit()
    {
    }
}