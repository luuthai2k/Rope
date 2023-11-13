using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
  
    public GameObject _weapon;
    public float dame;
    public AIMove aiMove;
    public bool isAttack;
    public bool isCombat;
    public bool isRanged;
    public AIController aiController;
    public void SeclectWeapon()
    {

    }
   
    public void Combat(SelectWeapon currenWeapon)
    {
        if (isCombat)
        {
            aiController.animator.SetLayerWeight(1, 0f);
            aiController.animator.SetBool("Strafe", true);
            transform.LookAt(aiController.enemy.transform);
            if (currenWeapon == SelectWeapon.Bat)
            {
                aiController.animator.SetInteger("MeleeWeaponType", (int)currenWeapon);
                aiController.animator.SetInteger("Bat Type", Random.Range(0, 4));
            }
            else
            {
                aiController.animator.SetInteger("MeleeWeaponType", 0);
                aiController.animator.SetInteger("Unarmed Type", Random.Range(0, 13));
            }
        }
        else
        {
            FinishActack(1f);
        }
       
       
    }
    public void Ranged(SelectWeapon currenWeapon)
    {
        if (isRanged)
        {
            aiController.animator.SetLayerWeight(1, 1f);
            aiController.animator.SetBool("Strafe", true);
            aiController.animator.SetInteger("MeleeWeaponType", (int)currenWeapon);
            transform.LookAt(aiController.enemy.transform);
            if (currenWeapon == SelectWeapon.Knife)
            {
                aiController.animator.SetInteger("Knife Type", Random.Range(0, 4));

            }
            if (currenWeapon == SelectWeapon.Pistol)
            {
                //_weapon.GetComponent<Gun>().StartShooting();
            }
            if (currenWeapon == SelectWeapon.Rifle)
            {

            }
            if (currenWeapon == SelectWeapon.Shotgun)
            {
                //_weapon.GetComponent<ShotGun>().StartShooting();
            }
            if (currenWeapon == SelectWeapon.Minigun)
            {

            }
        }
        else
        {
            FinishActack(1f);
        }
       
    }
    public void Attack(SelectWeapon currenWeapon)
    {
        StartCoroutine(ActackCouroutine(currenWeapon));
    }
    IEnumerator ActackCouroutine(SelectWeapon currenWeapon)
    {
        while (isAttack)
        {
            float distance = Vector3.Distance(aiController.enemy.transform.position, transform.position);
            Debug.LogError(distance + "Check");
            aiMove.pointtarget = aiController.enemy.transform;
            if (currenWeapon == SelectWeapon.Minigun || currenWeapon == SelectWeapon.Pistol || currenWeapon == SelectWeapon.Shotgun || currenWeapon == SelectWeapon.Rifle)
            {
                if (distance >= 20)
                {
                    aiController.timedelay = 2f;
                    FinishActack(1f);
                    isCombat = false;
                    isRanged = false;
                    aiMove.ismove = true;
                    aiMove.Move(1f);

                }
                if (distance < 20 && distance > 5f)
                {
                    aiController.timedelay = 1f;
                    isRanged = true;
                    isCombat = false;
                    Ranged(currenWeapon);
                }
                else
                {
                    isRanged = false;
                    aiController.timedelay = 0.5f;
                    if (distance > 2f)
                    {
                        FinishActack(0.5f);
                        isCombat = false;
                        aiMove.ismove = true;
                        aiMove.Move(1f);

                    }
                    else
                    {

                        isCombat = true;                     
                        aiMove.ismove = false;
                        Combat(currenWeapon);
                    }
                }
            }
            else
            {
               
              
                if (distance > 2f)
                {
                    FinishActack(1f);
                    isCombat = false;
                    aiMove.ismove = true;
                    aiMove.Move(1f);
                    if (distance < 5f)
                    {
                        aiController.timedelay = 0.5f;
                    }
                    else
                    {
                        aiController.timedelay =1f;
                    }
                }
                else
                {
                    aiController.timedelay = 0.5f;
                    isCombat = true;
                    aiMove.ismove = false;
                    Combat(currenWeapon);
                }
            }
            yield return new WaitForSeconds(aiController.timedelay);
        }


    }
    public void FinishActack(float delaytime)
    {
        StartCoroutine(CouroutineFinishActack(delaytime));
    }
    IEnumerator CouroutineFinishActack(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        aiController.animator.SetBool("Strafe", false);
    }
}
