using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
//using static UnityEditor.PlayerSettings;


public class NPCPooling : MonoBehaviour
{
    public static NPCPooling ins;

    [SerializeField]
    private List<GameObject> NPC = new List<GameObject>();

    [SerializeField]
    private List<GameObject> NPCPool = new List<GameObject>();

    [SerializeField]
    private List<PointAIMove> NPCAIMove = new List<PointAIMove>();

    [SerializeField]
    private List<GameObject> NPCStar;

    [SerializeField]
    private List<GameObject> NPCStarPool;

    [SerializeField]
    private List<Transform> transformSpawnPoint;

    [SerializeField]
    private int maxPoolNpcStar;

    [SerializeField]
    private int indexNpcStar;

    [SerializeField]
    private List<GameObject> NPCPickup;

    [SerializeField]
    private List<GameObject> NPCPickupPool;

    [SerializeField]
    private int maxNpcPickUp;

    [SerializeField]
    private List<GameObject> NPCQuest;

    [SerializeField]
    private List<GameObject> NPCQuestPool;

    [SerializeField]
    private int maxNpcQuest;

    [SerializeField]
    private int indexNpcQuest;

    [SerializeField]
    private List<GameObject> QuestCar;

    [SerializeField]
    private List<GameObject> QuestCarPool;

    [SerializeField]
    private int maxCarQuest;

    [SerializeField]
    private int indexCarQuest;

    public List<GameObject> Car;

    public List<GameObject> CarPool;

    public void Awake()
    {
        ins = this;
    }
    public void ReStartGame()
    {
        for (int i = 0; i < NPCAIMove.Count; i++)
        {
            NPCAIMove[i].Init();
        }
    }


    public GameObject GetPool(Vector3 pos)
    {
        NPCManager.ins.totalnpc++;

       
        for (int i = 0;i< NPCPool.Count;i++)
        {
            if (!NPCPool[i].activeSelf)
            {
                NPCPool[i].SetActive(true);
                NPCPool[i].transform.position = pos;

                NPCControl NPC = NPCPool[i].GetComponent<NPCControl>();
                NPC.npcHp.hp = 100;
                NPC.npcHp.isdead = false;
                return NPCPool[i];
            }
        }
        int total = Random.Range(0, NPC.Count);
        if (NPCPool.Count >= NPCManager.ins.maxOnNPC)
        {
            return null;
        }
        GameObject newnpc = Instantiate(NPC[total], pos, Quaternion.identity);

        newnpc.GetComponent<NPCControl>().npcHp.isPolice = false;
        NPCPool.Add(newnpc);
        return newnpc;

    }

    public void ReturnPool(GameObject npc, float time)
    {
        StartCoroutine(CouroutineReturnPool(npc, time));
    }
    IEnumerator CouroutineReturnPool(GameObject npc, float time)
    {
        yield return new WaitForSeconds(time);
        NPCManager.ins.totalnpc--;
        npc.transform.parent = null;

        npc.gameObject.GetComponent<Animator>().enabled = true;
        if (npc.GetComponent<NPCDriver>().Vehiclel != null)
        {      
            ReturnCar(npc.GetComponent<NPCDriver>().Vehiclel);
            npc.GetComponent<NPCDriver>().candriver = false;
        }

        npc.SetActive(false);
    }
    //NPCPolice

    public void ReturnPolice(GameObject npc, float time)
    {
        StartCoroutine(CouroutineReturnPolice(npc, time));
    }
    IEnumerator CouroutineReturnPolice(GameObject npc, float time)
    {
        yield return new WaitForSeconds(time);

        npc.gameObject.GetComponent<Animator>().enabled = true;

        npc.SetActive(false);
    }
    public void ResetNPCStar()
    {
        for (int i = 0; i < NPCStarPool.Count; i++)
        {
            if (NPCStarPool[i].activeSelf == false)
            {
                NPCStarPool[i].GetComponent<Animator>().enabled = true;
                NPCStarPool[i].SetActive(true);
                int indexSpawnPoint = Random.Range(0, transformSpawnPoint.Count);
                NPCStarPool[i].transform.position = transformSpawnPoint[indexSpawnPoint].position;
                NPCControl NPC = NPCStarPool[i].GetComponent<NPCControl>();
                NPC.pointtarget = Player.ins.transform;

                NPC.npcHp.hp = 100;
                NPC.npcHp.isdead = false;
                return;

            }
        }
    }
    public void SpawnStarNPC(int index)
    {

        if (index == 0)
        {
            if (indexNpcStar < maxPoolNpcStar)
            {
                int indexSpawnPoint = Random.Range(0, transformSpawnPoint.Count);

                if (Vector3.Distance(transformSpawnPoint[indexSpawnPoint].position, Player.ins.transform.position) > 50)
                {
                    SpawnStarNPC(index);
                    return;
                }

                GameObject newnpc = Instantiate(NPCStar[0], transformSpawnPoint[indexSpawnPoint].position, Quaternion.identity);
                PoliceStarManager.ins.AddPolice(newnpc);
                NPCControl NPC = newnpc.GetComponent<NPCControl>();
                NPCStarPool.Add(newnpc);
                NPC.npcState.ChangeState(SelectState.Attack);
                NPC.npcHp.isPolice = true;
                NPC.lastpoint = transformSpawnPoint[indexSpawnPoint];
                indexNpcStar++;
            }
            else
            {
                ResetNPCStar();
            }

        }
        else if (index == 1)
        {
            NPCStar[1].GetComponent<Animator>().enabled = true;
            NPCStar[1].SetActive(true);
            int indexSpawnPoint = Random.Range(0, transformSpawnPoint.Count);
            NPCStar[1].transform.position = transformSpawnPoint[indexSpawnPoint].position;

        }
        else if (index == 2)
        {
            NPCStar[2].GetComponent<Animator>().enabled = true;
            NPCStar[2].SetActive(true);
            int indexSpawnPoint = Random.Range(0, transformSpawnPoint.Count);
            NPCStar[2].transform.position = transformSpawnPoint[indexSpawnPoint].position;
        }

    }

   

    // Pickup
    public void SpawnPickUp(Vector3 posPickup, bool _isMoney = false)
    {
        if (_isMoney == false)
        {
            if (NPCPickupPool.Count >= maxNpcPickUp)
            {
                for (int i = 0; i < NPCPickupPool.Count; i++)
                {
                    if (NPCPickupPool[i].activeSelf == false)
                    {
                        NPCPickupPool[i].SetActive(true);

                        NPCPickupPool[i].transform.position = posPickup;
                        return;
                    }
                }
            }
            else
            {
                int index = Random.Range(1, 3);
                GameObject pickupNew = Instantiate(NPCPickup[index], posPickup, Quaternion.identity);

                NPCPickupPool.Add(pickupNew);
            }
        }

        else
        {
            if (NPCPickupPool.Count >= maxNpcPickUp)
            {
                for (int i = 0; i < NPCPickupPool.Count; i++)
                {
                    if (NPCPickupPool[i].activeSelf == false && NPCPickupPool[i].GetComponent<MoneyPickup>() != null)
                    {
                        NPCPickupPool[i].SetActive(true);

                        NPCPickupPool[i].transform.position = posPickup;
                        return;
                    }
                }
            }
            else
            {
                GameObject pickupNew = Instantiate(NPCPickup[0], posPickup, Quaternion.identity);

                NPCPickupPool.Add(pickupNew);
            }
        }
    }

    //NPCQuest
    public void SpawnNPCQuest(Vector3 pos)
    {
        if (indexNpcQuest < maxNpcQuest)
        {
            GameObject newnpc = Instantiate(NPCQuest[0], pos, Quaternion.identity);
            NPCQuestPool.Add(newnpc);
            NPCControl NPC = newnpc.GetComponent<NPCControl>();
            NPC.npcState.ChangeState(SelectState.Attack);
            NPC.npcHp.isNpcQuest = true;
            indexNpcQuest++;
        }
        else
        {
            ResetNPCQuest(pos);
        }
    }

    public void ReturnNpcQuest(GameObject NPC)
    {

        NPC.SetActive(false);
        NPC.GetComponent<Animator>().enabled = false;

    }

    public void ResetNPCQuest(Vector3 pos)
    {
        for (int i = 0; i < NPCStarPool.Count; i++)
        {
            if (NPCQuestPool[i].activeSelf == false)
            {
                NPCControl NPC = NPCQuestPool[i].GetComponent<NPCControl>();
                NPC.animator.enabled = true;
                NPCQuestPool[i].SetActive(true);

                NPCQuestPool[i].transform.position = pos;
                NPC.pointtarget = Player.ins.transform;
                NPC.npcHp.hp = 100;
                NPC.npcHp.isdead = false;
                return;
            }
        }
    }

    //CarQuest

    public void SpawnCarQuest(Vector3 pos)
    {
        if (indexCarQuest < maxCarQuest)
        {
            GameObject newnpc = Instantiate(QuestCar[0], pos, Quaternion.identity);
            QuestCarPool.Add(newnpc);
            indexCarQuest++;
        }
        else
        {
            ResetCarQuest(pos);
        }
    }

    public void ResetCarQuest(Vector3 pos)
    {
        for (int i = 0; i < NPCStarPool.Count; i++)
        {
            if (QuestCarPool[i].activeSelf == false)
            {

                QuestCarPool[i].SetActive(true);

                QuestCarPool[i].transform.position = pos;
                CarPool[i].GetComponent<VehiclesHp>().Init();
                return;
            }
        }
    }

    public void ReturnCarQuest(GameObject Car)
    {
        Car.SetActive(false);

    }

    //Car
    public GameObject SpawnCar(Vector3 pos)
    {
     
        if (SpawnNPCInit.ins.isSpawnVehicles)
        {
            GameObject newnpc = Instantiate(Car[0], pos, Quaternion.identity);
            Debug.LogError("Check");
            SpawnNPCInit.ins.Vehicles(1);
            CarPool.Add(newnpc);
        
            SpawnNPCInit.ins.onVehicles += 1;

            return newnpc;
        }
        else
        {    
            return ResetCar(pos);
        }
    }

    public void ReturnCar(GameObject Car)
    {
     
        Car.SetActive(false);
        SpawnNPCInit.ins.onVehicles -= 1;
        Car.GetComponent<Car>().npcDrive = null;

    }

    public GameObject ResetCar(Vector3 pos)
    {
     
        for (int i = 0; i < CarPool.Count; i++)
        {
          
            if (CarPool[i].GetComponent<Car>().npcDrive == null)
            {           
                CarPool[i].SetActive(true);
                CarPool[i].transform.position = pos;
                CarPool[i].GetComponent<Car>().vehiclesHp.Init();
                SpawnNPCInit.ins.onVehicles += 1;
                return CarPool[i];
            }
        }
        

        return null;
    }
    //Check

    public void CheckPlayerDead()
    {
        for (int i = 0; i < NPCPool.Count; i++)
        {
            if (NPCPool[i].GetComponent<NPCControl>().npcState.currentState == SelectState.Attack)
            {
                NPCPool[i].GetComponent<NPCControl>().npcState.ChangeState(SelectState.Move);
                NPCPool[i].GetComponent<NPCControl>().pointtarget = NPCPool[i].GetComponent<NPCControl>().lastpoint;
            }
            
        }
        if (NPCStarPool.Count != 0)
        {
            PoliceStarManager.ins.LoseWanterPoint();
            for (int i = 0; i < NPCStarPool.Count; i++)
            {
                ReturnPolice(NPCStarPool[i],0);
            }
        }

        if (NPCQuestPool.Count != 0)
        {
            for (int i = 0; i < NPCQuestPool.Count; i++)
            {
                ReturnNpcQuest(NPCQuestPool[i]);
            }
        }

        if (QuestCarPool.Count != 0)
        {
            for (int i = 0; i < NPCQuestPool.Count; i++)
            {
                ReturnCarQuest(QuestCarPool[i]);
            }
        }
    }

    public void CheckEndQuest()
    {
        if (NPCQuestPool.Count != 0)
        {
            for (int i = 0; i < NPCQuestPool.Count; i++)
            {
                ReturnNpcQuest(NPCQuestPool[i]);
            }
        }

        if (QuestCarPool.Count != 0)
        {
            for (int i = 0; i < NPCQuestPool.Count; i++)
            {
                ReturnCarQuest(QuestCarPool[i]);
            }
        }
    }
    public void EndStarPolice()
    {
        if (NPCStarPool.Count != 0)
        {
            for (int i = 0; i < NPCStarPool.Count; i++)
            {
                NPCControl NPC = NPCStarPool[i].GetComponent<NPCControl>();
                NPC.npcState.ChangeState(SelectState.Move);
                NPC.pointtarget = NPC.lastpoint;

            }
        }
    }

}
