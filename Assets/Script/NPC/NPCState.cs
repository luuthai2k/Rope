using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCState : MonoBehaviour
{
    public NPCControl npcControl;
    public SelectState currentState;

    //private void OnEnable()
    //{
    //    HandleStateChange();
    //}
    public void ChangeState(SelectState newState)
    {
        currentState = newState;
        HandleStateChange();
    }

    public void HandleStateChange()
    {
        // Based on the new state, trigger appropriate actions in the AIController
        switch (currentState)
        {
            case SelectState.Idle:
                npcControl.DoIdleAction();
                break;
            case SelectState.Move:
                npcControl.DoMoveAction();
                break;
            case SelectState.Run:
                npcControl.DoRunAction();
                break;
            case SelectState.Fall:
                npcControl.DoFallAction();
                break;
            case SelectState.Attack:    
                npcControl.DoAttackAction();
                break;
            case SelectState.Driver:
                npcControl.DoDriver();
                break;
            default:
                break;
        }
    }
}
public enum SelectState
{
    Move,
    Idle,
    Run,
    Fall,
    Attack,
    Driver

}