using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickup : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player.ins.playerHP.PlusArmor(10);

            gameObject.SetActive(false);

            
        }
    }
}
