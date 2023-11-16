using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float hp, firstHp;

    public float stamina, firstStamina;

    [SerializeField]
    private float armor, maxArmor;


    private bool isDead;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject ragdoll;

    [SerializeField]
    private GameObject ragdollNow;

    [SerializeField]
    private Vector3 ragdollPosition;

    [SerializeField]
    private Vector3 firstPosition;

    [SerializeField]
    private CharacterParameter characterParameter;


    [SerializeField]
    private Rigidbody rb;
    private bool canRestoreStamina;

    public bool infiniteStamina;
    public void Start()
    {
        firstPosition = transform.position;
        //effectblood.Stop();

        RechargeStamina();
        ChangeHp();
        hp = UpgradeXpManager.ins.dataGame.maxHp;
        firstHp = hp;
        stamina = UpgradeXpManager.ins.dataGame.maxStamina;
        firstStamina = stamina;
        armor = 0;
        maxArmor = firstHp / 2;
        characterParameter.HeatIndex(firstHp, hp);
        characterParameter.Armor(maxArmor, armor);
        characterParameter.StaminaIndex(firstStamina, stamina);
    }


    public void OnHit(HitDameState dameState, bool isRagdoll, float dame, Vector3 pos, float powerRagdoll = 0)
    {
        if (isDead == false)
        {
            if (dameState == HitDameState.Car)
            {
                hp -= dame;

            }
            else if (dameState == HitDameState.Water)
            {
                if (stamina > 0)
                {
                    LoseStamina(dame);
                }
                else
                {

                    hp -= dame;
                }
            }
            else if (dameState == HitDameState.Weapon)
            {
                //effectblood.Play();
                if (armor <= 0)
                {
                    hp -= dame;
                }
                else
                {
                    LoseArmor(dame);
                }
            }

            if (hp <= 0)
            {
                isDead = true;
                OnDead();
            }
            if (isRagdoll)
            {
                OnRagdoll(Vector3.zero, powerRagdoll);
            }

            characterParameter.HeatIndex(firstHp, hp);

        }

    }


    public void OnRagdoll(Vector3 pos, float power = 0)
    {
        player.SetActive(false);

        ragdollNow = Instantiate(ragdoll, transform.position, transform.rotation);
        gameObject.GetComponent<CharacterController>().enabled = false;

        rb.AddForce(pos * power);

        Invoke("EndRagdoll", 1.25f);
    }


    public void EndRagdoll()
    {
        transform.position = new Vector3(ragdollNow.transform.position.x, transform.position.y, ragdollNow.transform.position.z);

        player.SetActive(true);

        gameObject.GetComponent<CharacterController>().enabled = true;
        Destroy(ragdollNow);
    }

    public void OnDead()
    {
        player.SetActive(false);
        QuestManager.ins.LoseQuest();
        ragdollNow = Instantiate(ragdoll, transform.position, transform.rotation);
        gameObject.GetComponent<CharacterController>().enabled = false;
        transform.parent = null;
        Player.ins.ChangeControl(0);
        Player.ins.animator.Play("Grounded");
        CameraManager.ins.ChangeCam(0, Player.ins.transform);
        Invoke("EndDead", 1.25f);
    }

    public void EndDead()
    {

        hp = firstHp;
        stamina = firstStamina;
        isDead = false;
        characterParameter.HeatIndex(firstHp, hp);
        characterParameter.StaminaIndex(firstStamina, stamina);
        player.SetActive(true);
        transform.position = firstPosition;
        gameObject.GetComponent<CharacterController>().enabled = true;
        NPCPooling.ins.CheckPlayerDead();
        Destroy(ragdollNow);

    }

    public void LoseStamina(float _stamina)
    {
        stamina -= _stamina;
        if (stamina <= 0)
        {
            stamina = 0;
        }
        characterParameter.StaminaIndex(firstStamina, stamina);
        canRestoreStamina = false;
        if (!canRestoreStamina)
        {
            CancelInvoke("CanRestoreStamina");
        }
        Invoke("CanRestoreStamina", 2f);
    }

    private void CanRestoreStamina()
    {
        canRestoreStamina = true;
    }
    public void RechargeStamina()
    {
        if (canRestoreStamina)
        {
            if (Player.ins.playerControl.playerState == PlayerState.Swimming)
            {

            }
            else if (Player.ins.playerControl.playerState == PlayerState.Laser)
            {

            }
            else
            {
                PlusStamina(UpgradeXpManager.ins.dataGame.maxStaminaRecharge);
            }
        }
        Invoke("RechargeStamina", 1f);
    }

    public void PlusStamina(float _stamina, bool maxStamina = false)
    {
        if (maxStamina)
        {
            stamina = firstStamina;

            if (stamina >= firstStamina)
            {
                stamina = firstStamina;
            }
            characterParameter.StaminaIndex(firstStamina, stamina);
        }
        else
        {
            stamina += _stamina;

            if (stamina >= firstStamina)
            {
                stamina = firstStamina;
            }
            characterParameter.StaminaIndex(firstStamina, stamina);
        }

    }
    public void InfiniteStamina()
    {
        infiniteStamina = true;

        Invoke("EndInfiniteStamina", 30f);
    }

    public void EndInfiniteStamina()
    {
        infiniteStamina = false;
    }

    public void LoseArmor(float _armor)
    {
        armor -= _armor;

        if (armor <= 0)
        {
            armor = 0;
        }
        else if (armor >= maxArmor)
        {
            armor = maxArmor;
        }

        characterParameter.Armor(maxArmor, armor);
    }
    public void PlusArmor(int _armor)
    {
        armor = maxArmor;

        characterParameter.Armor(maxArmor, armor);
    }


    public void PlusHp(int _hp)
    {
        hp += _hp;

        if (hp >= firstHp)
        {
            hp = firstHp;
        }
        characterParameter.HeatIndex(firstHp, hp);
    }

    public void ChangeHp()
    {
        if (Player.ins.playerControl.playerState == PlayerState.Swimming)
        {

        }
        else
        {
            PlusHp(10);
        }

        Invoke("ChangeHp", 1f);
    }

}

public enum HitDameState
{
    Car,
    Water,
    Weapon,
    Explosion
}
