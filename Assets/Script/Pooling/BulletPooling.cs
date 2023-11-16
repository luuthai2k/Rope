using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    public static BulletPooling ins;
    public GameObject rocket;
    public List<GameObject> rocketPool = new List<GameObject>();
    public GameObject laser;
    public List<GameObject> laserPool = new List<GameObject>();
    public GameObject rocketHeli;
    public List<GameObject> rocketHeliPool = new List<GameObject>();
   
    void Start()
    {
        ins = this;
    }
    public GameObject GetRocketHeliPool(Vector3 pos)
    {
        return GetPool(pos, rocket, rocketHeliPool);
    }
    public GameObject GetRocketPool(Vector3 pos)
    {
        return GetPool(pos, rocket, rocketPool);
    }
    public GameObject GetLaserPool(Vector3 pos)
    {
        return GetPool(pos, laser, laserPool);
    }
    public GameObject GetPool(Vector3 pos, GameObject poolObj, List<GameObject> Pool)
    {
        foreach (var pool in Pool)
        {

            if (!pool.activeInHierarchy)
            {
                pool.SetActive(true);
                pool.transform.position = pos;
                pool.transform.rotation = Quaternion.identity;
                return pool;
            }
        }
        GameObject newpoolObj = Instantiate(poolObj, pos, Quaternion.identity);
        Pool.Add(newpoolObj);
        return newpoolObj;
    }
}
