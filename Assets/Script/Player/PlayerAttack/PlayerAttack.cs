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
    public void Attack(bool isAttack)
    {
        if (isAttack||Input.GetKey(KeyCode.A))
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
        else
        {
            Player.ins.animator.SetBool("IsRun", false);
            Player.ins.animator.SetBool("IsAttack", false);
        }

    }
    public void Unarmed()
    {
        Player.ins.animator.SetInteger("UnarmedType", Random.Range(0, 13));
        Player.ins.animator.SetBool("IsAttack", true);
        if (enemy != null)
        {
            Vector3 directionToEnemy = enemy.transform.position - transform.parent.transform.position;
            Quaternion rot = Quaternion.LookRotation(directionToEnemy);
            transform.parent.transform.rotation = Quaternion.Euler(0, rot.eulerAngles.y, 0);

            if (directionToEnemy.magnitude > 1)
            {
                if (directionToEnemy.magnitude > 15 || !enemy.activeSelf)
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
              enemy = Player.ins.playerSensor.ReturnHuman();
           

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
