using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class RagDoll : MonoBehaviour
{

    [SerializeField]
    private GameObject character;
    [SerializeField]
    private BodyPhysicsController bodyPhysics;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float timeRagdoll;

    public void OnRagDoll(GameObject _character, Vector3 pos, bool _isDie = false)
    {
        character = _character;
        rb.AddForce(pos);
        bodyPhysics.Fall(_isDie);


        if(_isDie == false)
        {
            Invoke("EndRagdoll", 6f);
        }
        else
        {
            Invoke("DieNPC", 4.5f);
        }
       
    }


    public void DieNPC()
    {
    
        Destroy(gameObject);
    }

    public void EndRagdoll()
    {
        character.transform.position = transform.position;
        character.transform.rotation = transform.rotation;

        character.SetActive(true);

        if (character.GetComponent<CharacterController>() != null)
        {
            character.GetComponent<CharacterController>().enabled = true;
        }
        //character.GetComponent<Animator>().Play("Grounded");
        Destroy(gameObject);

        if (character.GetComponent<NPCControl>() != null)
        {
            character.GetComponent<NPCControl>().npcState.ChangeState(SelectState.Attack);
        }

    }
}

