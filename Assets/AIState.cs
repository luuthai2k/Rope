using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    public AIController aiController;
    public SelectState currentState;
   
    private void OnEnable()
    {
        HandleStateChange();
    }
    public void ChangeState(SelectState newState)
    {
        currentState = newState;
        HandleStateChange();
    }

    private void HandleStateChange()
    {
        // Based on the new state, trigger appropriate actions in the AIController
        switch (currentState)
        {
            case SelectState.Idle:
                aiController.DoIdleAction();
                break;
            case SelectState.Move:
                aiController.DoMoveAction();
                break;
            case SelectState.Run:
                aiController.DoRunAction();
                break;
            case SelectState.Fall:
                aiController.DoFallAction();
                break;
            case SelectState.Attack:
                aiController.DoAttackAction();
                break;
            default:
                break;
        }
    }
}

