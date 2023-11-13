using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGunMovement : MonoBehaviour
{
    
 
    public  GameObject mainGun;
    public GameObject barrel;
    public int maxGunAngle_elevation = 35;
    public int minGunAngle_depression = 8;
    public Transform target;
    public float speed;

  
    

    private void Update()
    {
        
            MoveTurret();
       
    }

    private void MoveTurret()
    {
       
       
        mainGun.transform.rotation = Quaternion.RotateTowards(mainGun.transform.rotation, Quaternion.Euler(new Vector3(0f, Camera.main.transform.rotation.eulerAngles.y, 0f)), speed * Time.deltaTime);

       
        Vector3 directionToTarget = target.position-barrel.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);  
      
        float targetXAngle = targetRotation.eulerAngles.x;

        if (targetXAngle > 180f)
        {
            targetXAngle -= 360f;
        }
        float clampedXAngle = Mathf.Clamp(targetXAngle, maxGunAngle_elevation, minGunAngle_depression);
        barrel.transform.localRotation = Quaternion.Euler(clampedXAngle, 0f, 0f);
        Debug.Log(targetXAngle);


    }


}
