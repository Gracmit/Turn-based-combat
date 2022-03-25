using UnityEngine;

public class Win : IState
{
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        Debug.Log("Win");
    }

    public void OnExit()
    {
    }
}