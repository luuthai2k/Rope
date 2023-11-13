using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;

public class EGA_Laser : MonoBehaviour
{
    public GameObject HitEffect;
    public float HitOffset = 0;

    public float MaxLength;
    public LineRenderer Laser;

    public float MainTextureLength = 1f;
    public float NoiseTextureLength = 1f;
    private Vector4 Length = new Vector4(1,1,1,1);
    private bool LaserSaver = false;
    private bool UpdateSaver = false;
    public ParticleSystem[] Effects;
    private ParticleSystem[] Hit;
    public float dame;
    public float time;
    public float timeDelay=0.2f;
    void Start ()
    {
        Effects = GetComponentsInChildren<ParticleSystem>();
        Hit = HitEffect.GetComponentsInChildren<ParticleSystem>();
       
    }

    void Update()
    {
        Laser.material.SetTextureScale("_MainTex", new Vector2(Length[0], Length[1]));                    
        Laser.material.SetTextureScale("_Noise", new Vector2(Length[2], Length[3]));
        if (Laser != null && UpdateSaver == false)
        {
            
            Laser.SetPosition(0, transform.position);
            Laser.SetPosition(1, PointCenterSceenToWorld.ins.targetTransform.position);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, PointCenterSceenToWorld.ins.targetTransform.position-transform.position, out hit, MaxLength))
            {
                HitEffect.transform.position = hit.point + hit.normal * HitOffset;
                HitEffect.transform.rotation = Quaternion.identity;
                foreach (var AllPs in Effects)
                {
                    if (!AllPs.isPlaying) AllPs.Play();
                }
                Length[0] = MainTextureLength * (Vector3.Distance(transform.position, hit.point));
                Length[2] = NoiseTextureLength * (Vector3.Distance(transform.position, hit.point));
                CheckHitDame(hit.collider.gameObject);
            }
            else
            {
                var EndPos = transform.position + transform.forward * MaxLength;
                HitEffect.transform.position = EndPos;
                foreach (var AllPs in Hit)
                {
                    if (AllPs.isPlaying) AllPs.Stop();
                }
                Length[0] = MainTextureLength * (Vector3.Distance(transform.position, EndPos));
                Length[2] = NoiseTextureLength * (Vector3.Distance(transform.position, EndPos));
            }
            if (Laser.enabled == false && LaserSaver == false)
            {
                LaserSaver = true;
                Laser.enabled = true;
            }
        }  
    }

    public void DisablePrepare()
    {
        if (Laser != null)
        {
            Laser.enabled = false;
        }
        UpdateSaver = true;
        if (Effects != null)
        {
            foreach (var AllPs in Effects)
            {
                if (AllPs.isPlaying) AllPs.Stop();
            }
        }
    }
    public void CheckHitDame(GameObject other)
    {
        time += Time.deltaTime;
        if (time <= timeDelay) return;
        time = 0;
    
        if (other.gameObject.layer == 9)
        {

            //Player.ins.playerHP.OnHit(HitDameState.Weapon, true, dameExplosion,pos,powerRagDoll);

        }
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<VehiclesHp>().DameVehicles(Mathf.Clamp(dame, 10, dame));
        }

        if (other.gameObject.layer == 10)
        {
            
            other.gameObject.GetComponent<NPCHP>().HitDame(Mathf.Clamp(dame, 10, dame), Vector3.zero);

        }
    }
}
