using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxShotPooling : MonoBehaviour
{
    public static FxShotPooling ins;

    [SerializeField]
    private GameObject _shotPistol;
    [SerializeField]
    private List<GameObject> shotPistolPool = new List<GameObject>();

    [SerializeField]
    private GameObject _shotGun;
    [SerializeField]
    private List<GameObject> shotGunPool = new List<GameObject>();

    private void Awake()
    {
        ins = this;
    }
    public GameObject GetShotPistolPool(Vector3 pos)
    {
        return GetPool(pos, _shotPistol, shotPistolPool);
    }
    
    public GameObject GetShotGunPool(Vector3 pos)
    {
        return GetPool(pos, _shotGun, shotGunPool);
    }
    

    public GameObject GetPool(Vector3 pos, GameObject particleSystem, List<GameObject> Pool)
    {
        foreach (var particle in Pool)
        {
            particleSystem.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (!particle.gameObject.activeInHierarchy)
            {
                particle.SetActive(true);
                
                particle.transform.position = pos;
          
                
                particle.transform.rotation = Quaternion.identity;
                ReturnPool(particle, 3f);
                return particle;
            }
        }
        GameObject newparticle = Instantiate(particleSystem, pos, Quaternion.identity);
  
        Pool.Add(newparticle);
        ReturnPool(newparticle, 3f);
        return newparticle;
    }

    public void ReturnPool(GameObject particleSystem, float time)
    {
        StartCoroutine(CouroutineReturnPool(particleSystem, time));
    }
    IEnumerator CouroutineReturnPool(GameObject particleSystem, float time)
    {
        //particleSystem.GetComponent<TrailRenderer>().enabled = false;
        yield return new WaitForSeconds(time);
        //particleSystem.GetComponent<TrailRenderer>().enabled = true;
        particleSystem.SetActive(false);

    }

}
