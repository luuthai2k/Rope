using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public static AIController ins;
    public AIState aiState;
    public ChacractorData chacractorData;
    public LayerMask obstacleMask;
    public GameObject enemy;
    public Animator animator;
    public AIMove aiMove;
    public AIAttack aiAttack;
    public float timedelay;
    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
     
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            aiState.ChangeState(SelectState.Attack);
        }
    }
    public void DoIdleAction()
    {
        aiMove.Idle(0f);
    }

    public void DoMoveAction()
    {
        timedelay = 2f;
        aiMove.ismove = true;
        aiMove.Move(0.5f);
    }

    public void DoRunAction()
    {
        // Implement running behavior
    }

    public void DoFallAction()
    {
        // Implement falling behavior
    }

    public void DoAttackAction()
    {
        aiAttack.isAttack = true;
        aiAttack.Attack(chacractorData.rangedWeapons);

    }
   
}
