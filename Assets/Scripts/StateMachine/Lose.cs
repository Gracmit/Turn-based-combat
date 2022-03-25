using UnityEngine;

public class Lose: IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        Debug.Log("Lose");
    }

    public void OnExit()
    {
    }
}