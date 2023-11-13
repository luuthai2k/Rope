using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;


public class NPCControl : MonoBehaviour
{
    public static NPCControl ins;
    public NavMeshAgent navMeshAgent;
    public NPCState npcState;
    public ChacractorData chacractorData;
    public LayerMask obstacleMask;
    public GameObject enemy;
    public Animator animator;
    public NPCMovement npcMovement;
    public Transform pointtarget;
    public Transform lastpoint;
    public NPCAttack npcAttack;
    public NPCDriver npcDriver;
    public float timedelay;
    public NPCHP npcHp;

    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        DistancePlayer();
        
    }
   

    public void DistancePlayer()
    {
        float distance = Vector3.Distance(transform.position, Player.ins.transform.position);

        if(distance >= 100)
        {
            
            if(npcHp.isPolice == false)
            {
                NPCManager.ins.npcPooling.ReturnPool(gameObject, 0f);
            }
            else
            {
                //NPCManager.ins.npcPooling.ReturnStarNpc(gameObject);
            }
            
        }
        Invoke("DistancePlayer", 1f);
    }
    public void DoIdleAction()
    {
        npcMovement.canmove = false;
        npcMovement.Idle();

    }

    public void DoMoveAction()
    {
        timedelay = 1f;
        npcMovement.canmove = true;
        npcMovement.Move(0.5f);
        animator.SetBool("Strafe", false);
       
        npcDriver.isDriver = false;
    }

    public void DoRunAction()
    {
        // Implement running behavior
    }

    public void DoFallAction()
    {
        npcHp.OnRagdoll(new Vector3(4, -4, 4));
    }

    public void DoAttackAction()
    {
        if (enemy == null)
        {
            enemy = Player.ins.gameObject;
        }
        npcAttack.isAttack = true;
        npcMovement.canmove = false;
        npcAttack.Attack(chacractorData.meleeWeapons, chacractorData.rangedWeapons);

    }
    public void DoDriver()
    {
       
        npcDriver.candriver = true;
        npcDriver.isDriver = true;
        npcDriver.DriverVehicle();
        
    }
}
