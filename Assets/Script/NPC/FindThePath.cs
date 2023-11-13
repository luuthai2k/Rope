using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
//using static Unity.VisualScripting.Member;
//using static UnityEditor.PlayerSettings;

public class FindThePath : MonoBehaviour
{
    
    [Header("NavMesh")]
    public List<string> NavMeshLayers;
    private int NavMeshLayerBite;
    public List<Vector3> waypoints = new List<Vector3>();
    public Vector3 PostionToFollow = Vector3.zero;
    [Header("Debug")]
    public bool ShowGizmos;
    private int currentWayPoint;
    public float AIFOV = 60;
   
    
   

    void Start()
    {
        currentWayPoint = 0;
        //CalculateNavMashLayerBite();
    }
   
    public void ClearWayPoint()
    {
        currentWayPoint = 0;
        waypoints.Clear();
    }
    
   
    private void CalculateNavMashLayerBite()
    {
        if (NavMeshLayers == null || NavMeshLayers[0] == "AllAreas")
            NavMeshLayerBite = NavMesh.AllAreas;
        else if (NavMeshLayers.Count == 1)
            NavMeshLayerBite += 1 << NavMesh.GetAreaFromName(NavMeshLayers[0]);
        else
        {
            foreach (string Layer in NavMeshLayers)
            {
                int I = 1 << NavMesh.GetAreaFromName(Layer);
                NavMeshLayerBite += I;
            }
        }
    }

    public void PathProgress(Transform pointtarget,Transform _transform) 
    {
        wayPointManager();
        ListOptimizer();

        void wayPointManager()
        {
          

            if (currentWayPoint < waypoints.Count) 
            {

                PostionToFollow = waypoints[currentWayPoint];
                if (Vector3.Distance(_transform.position, PostionToFollow) < 2)
                    currentWayPoint++;
                if (Vector3.Distance(waypoints[waypoints.Count-1], pointtarget.position) > 10)
                {
                    //currentWayPoint = 0;
                    //waypoints.Clear();
                }

                
            }

            if (currentWayPoint >= waypoints.Count - 3)
                CreatePath();
        }

        void CreatePath()
        {
            if (pointtarget == null)
            {
               
                RandomPath();
               
            }
            else
                CustomPath(pointtarget);

        }

        void ListOptimizer()
        {
            if (currentWayPoint > 1 && waypoints.Count > 30)
            {
                waypoints.RemoveAt(0);
                currentWayPoint--;
            }
        }
    }

    public void RandomPath( ) 
    {
        //NavMeshPath path = new NavMeshPath();
        //Vector3 sourcePostion;
        //NavMeshLayerBite = NavMesh.AllAreas;
        //if (waypoints.Count == 0)
        //{
        //    Vector3 randomDirection = Random.insideUnitSphere * 100;
        //    randomDirection += transform.position;
        //    sourcePostion = transform.position;
        //    Calculate(randomDirection, sourcePostion, transform.forward, NavMeshLayerBite);
        //}
        //else
        //{
        //    sourcePostion = waypoints[waypoints.Count - 1];
        //    Vector3 randomPostion = Random.insideUnitSphere * 100;
        //    randomPostion += sourcePostion;
        //    Vector3 direction = (waypoints[waypoints.Count - 1] - waypoints[waypoints.Count - 2]).normalized;
        //    Calculate(randomPostion, sourcePostion, direction, NavMeshLayerBite);
        //}


        //void Calculate(Vector3 destination, Vector3 sourcePostion, Vector3 direction, int NavMeshAreaByte)
        //{
        //    if (NavMesh.SamplePosition(destination, out NavMeshHit hit, 150, 1 << NavMesh.GetAreaFromName(NavMeshLayers[0])) &&
        //        NavMesh.CalculatePath(sourcePostion, hit.position, NavMeshAreaByte, path) && path.corners.Length > 2)
        //    {
        //        if (CheckForAngle(path.corners[1], sourcePostion, direction))
        //        {

        //            waypoints.AddRange(path.corners.ToList());
                  
        //        }
        //        else
        //        {
        //            if (CheckForAngle(path.corners[2], sourcePostion, direction))
        //            {
        //                waypoints.AddRange(path.corners.ToList());

        //            }
        //            else
        //            {
                       
                       
        //            }
        //        }
        //    }
        //    else
        //    {
               
               
        //    }
        //}
    }

    public void CustomPath(Transform destination) //Creates a path to the Custom destination
    {
        NavMeshPath path = new NavMeshPath();
        Vector3 sourcePostion;
        if (NavMesh.SamplePosition(destination.position, out NavMeshHit hit, 0.1f, 1 << NavMesh.GetAreaFromName("Road")))
        {
            NavMeshLayerBite = 1 << NavMesh.GetAreaFromName("Road");
            
        }
        else
        {
            NavMeshLayerBite += 1 << NavMesh.GetAreaFromName("Walkable");
        }
        if (waypoints.Count == 0)
        {
            sourcePostion = transform.position;
            Calculate(destination.position, sourcePostion, transform.forward, NavMeshLayerBite);
        }
        else
        {
            sourcePostion = waypoints[waypoints.Count - 1];
            Vector3 direction = (waypoints[waypoints.Count - 1] - waypoints[waypoints.Count - 2]).normalized;
            Calculate(destination.position, sourcePostion, direction, NavMeshLayerBite);
        }

        void Calculate(Vector3 destination, Vector3 sourcePostion, Vector3 direction, int NavMeshAreaBite)
        {
            if (NavMesh.SamplePosition(destination, out NavMeshHit hit, 200, NavMeshAreaBite) &&
                NavMesh.CalculatePath(sourcePostion, hit.position, NavMeshAreaBite, path))
            {
                if (path.corners.ToList().Count() > 1)
                {

                    waypoints.AddRange(path.corners.ToList());
                   
                 
                }
                else
                {
                    if (path.corners.Length > 2 )
                    {
                        waypoints.AddRange(path.corners.ToList());

                    
                    }
                    else
                    {
                       
                       
                    }
                }
            }
            else
            {
              
               
            }
        }
    }

    private bool CheckForAngle(Vector3 pos, Vector3 source, Vector3 direction) //calculates the angle between the car and the waypoint 
    {
        Vector3 distance = (pos - source).normalized;
        float CosAngle = Vector3.Dot(distance, direction);
        float Angle = Mathf.Acos(CosAngle) * Mathf.Rad2Deg;
       
        if (Angle < AIFOV)
            return true;
        else
            return false;
    }
    private void OnDrawGizmos() 
    {
        if (ShowGizmos == true)
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                if (i == currentWayPoint)
                    Gizmos.color = Color.blue;
                else
                {
                    if (i > currentWayPoint)
                        Gizmos.color = Color.red;
                    else
                        Gizmos.color = Color.green;
                }
                Gizmos.DrawWireSphere(waypoints[i], 2f);
            }
            CalculateFOV();
        }

        void CalculateFOV()
        {
          
            Gizmos.color = Color.white;
            float totalFOV = AIFOV * 2;
            float rayRange = 10.0f;
            float halfFOV = totalFOV / 2.0f;
            Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
            Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
            Vector3 leftRayDirection = leftRayRotation *transform.forward;
            Vector3 rightRayDirection = rightRayRotation * transform.forward;
            Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
            Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);
        }
    }
   
}
