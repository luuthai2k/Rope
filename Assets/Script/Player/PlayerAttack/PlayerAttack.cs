using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public SelectWeapon selectWeapon;
    public GameObject _weapon;
    public List<Collider> hitBoxs;
    public GameObject enemy;
    public float dame;
    public bool ishit;
    public void Attack()
    {
        Player.ins.animator.SetBool("Strafe", true);
        Player.ins.animator.SetInteger("MeleeWeaponType", (int)selectWeapon);
        if (selectWeapon == SelectWeapon.Unarmed)
        {
            Unarmed();
        }
        if (selectWeapon == SelectWeapon.Bat)
        {
            Player.ins.animator.SetInteger("BatType", Random.Range(0, 4));
        }
        if (selectWeapon == SelectWeapon.Knife)
        {
            Player.ins.animator.SetInteger("KnifeType", Random.Range(0, 3));
        }
        if (selectWeapon == SelectWeapon.Pistol)
        {
            _weapon.GetComponent<Gun>().StartShooting();
        }
        if (selectWeapon == SelectWeapon.Rifle)
        {

        }
        if (selectWeapon == SelectWeapon.Shotgun)
        {
          
        }
        if (selectWeapon == SelectWeapon.Minigun)
        {

        }
       
    }
    public void Unarmed()
    {
        Player.ins.animator.SetInteger("UnarmedType", Random.Range(0, 13));
        if(enemy != null)
        {
            Vector3 directionToEnemy = enemy.transform.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(directionToEnemy);
            transform.rotation = Quaternion.Euler(0,rot.eulerAngles.y,0);
           
            if (directionToEnemy.magnitude > 2)
            {
                if (directionToEnemy.magnitude > 10||enemy.activeSelf==false)
                {
                    enemy = null;
                    return;
                }
                else
                {
                    Player.ins.animator.SetBool("IsRun", true);
                }
               
            }
            else
            {
                Player.ins.animator.SetBool("IsRun", false);
            }
        }
        else
        {
            if (Player.ins.playerSensor.ReturnHuman() != null)
            {
                enemy = Player.ins.playerSensor.ReturnHuman();
            }
           
        }
    }
    public void FinishActack(float delaytime)
    {
        OffHitBox();
        StartCoroutine(CouroutineFinishActack(delaytime));
    }
    IEnumerator CouroutineFinishActack(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        Animator animator = Player.ins.animator;
        animator.SetBool("Strafe", false);
    }
    public void OffHitBox()
    {

        foreach (var hitbox in hitBoxs)
        {
            hitbox.enabled = false;
        }
    }
    public void OnHitBox()
    {
        foreach (var hitbox in hitBoxs)
        {
            Debug.LogError("on");
            hitbox.enabled = true;
        }
    }
}
public enum SelectWeapon
{
    Unarmed,
    Bat,
    Knife,
    Pistol,
    Rifle,
    Shotgun,
    Minigun


}
