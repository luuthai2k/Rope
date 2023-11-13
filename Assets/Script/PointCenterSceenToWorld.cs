using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCenterSceenToWorld : MonoBehaviour
{

    public static PointCenterSceenToWorld ins;
    public Transform targetTransform;
    public GameObject CollisionObj;
    private Camera _mainCamera;
    public float mindis;
    public float maxdis;
    Ray ray;
    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        ray.origin = _mainCamera.transform.position;
        ray.direction = _mainCamera.transform.forward;



        if (Physics.Raycast(ray, out RaycastHit raycastHit, maxdis, GameManager.ins.layerData.AimColliderMask))
        {
            float distance = Vector3.Distance(_mainCamera.transform.position, raycastHit.point) - Vector3.Distance(_mainCamera.transform.position, Player.ins.transform.position);
            if (distance > mindis)
            {
                targetTransform.position = raycastHit.point;
                ControlsManager.ins.Control[0].GetComponent<CharacterControl>().rope.SetActive(true);
                CollisionObj = raycastHit.collider.gameObject;

            }
            else
            {
                targetTransform.localPosition = new Vector3(0, 0, 200);
                ControlsManager.ins.Control[0].GetComponent<CharacterControl>().rope.SetActive(false);
            }






        }
        else
        {
            ControlsManager.ins.Control[0].GetComponent<CharacterControl>().rope.SetActive(false);
        }

    }
}
