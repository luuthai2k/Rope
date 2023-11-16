using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class VehiclesHp : MonoBehaviour
{
    [SerializeField]
    private float hp;
    private float fisrtHp;

    [SerializeField]
    private Car car;

    [SerializeField]
    private GameObject vehiclesExplosion;

    [SerializeField]
    private GameObject vehiclesNomal;

    [SerializeField]
    private bool isExplosion;

    public void Start()
    {
        Init();
        fisrtHp = hp;
    }

    public void Init()
    {

        vehiclesExplosion.SetActive(false);

        vehiclesNomal.SetActive(true);

        if(car != null) { car.enabled = true; }

        DistancePlayer();

        hp = fisrtHp;

        isExplosion = true;
    }

    public void DistancePlayer()
    {
        float distance = Vector3.Distance(transform.position, Player.ins.transform.position);

        if (distance >= 50 && car._driver == null)
        {
            //NPCManager.ins.npcPooling.ReturnPool(gameObject, 0.1f);
        }
        else
        {
            Invoke("DistancePlayer", 1f);
        }

        if (!car.gameObject.activeSelf && car._driver != null)
        {
            NPCPooling.ins.ReturnPool(car._driver, 0f);
            car._driver.transform.parent = null;
            car._driver = null;
            
        }
    }

    public void DameVehicles(float _hp)
    {
        hp -= _hp;
        if (car._driver != null)
        {
            car._driver.GetComponent<NPCDriver>().driverType = DriverType.Runaway;
        }
        

        if (hp <= 0)
        {
            vehiclesExplosion.SetActive(true);

            vehiclesNomal.SetActive(false);

            car.enabled = false;

            car.npcDrive.npcHp.HitDame(200, Vector3.zero);

            if (isExplosion)
            {
                ParticleSystem hit = FxPooling.ins.GetrocketHitPool(transform.position);

                isExplosion = false;
            }
            

            Invoke("ReturnVehicles", 3f);
        }

    }

    //public void ReturnVehicles()
    //{
    //    if (Vector3.Distance(transform.position, Player.ins.transform.position) > 100)
    //    {
    //        NPCPooling.ins.ReturnCar(transform.parent.gameObject);

    //        //SpawnNPCInit.ins.Vehicles(-1);

    //        Init();

    //    }
    //    else
    //    {
    //        Invoke("ReturnVehicles", 1f);
    //    }
       
    //}

}
