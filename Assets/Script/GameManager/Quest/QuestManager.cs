using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager ins;

    [SerializeField]
    private List<DataQuest> dataQuests;

    [SerializeField]
    private DataQuest dataQuest;

    [SerializeField]
    private List<Quest> listQuest;

    [SerializeField]
    private Quest quest;

    [SerializeField]
    private int indexQuest;

    public bool isQuest;

    [SerializeField]
    private List<Transform> startQuestPoint;

    [SerializeField]
    private int timeQuest;

    [SerializeField]
    private int enemyDie;

    [SerializeField]
    private int pickupGive;

    [SerializeField]
    private int pointQuest;

    [SerializeField]
    private int carDestroy;

    [SerializeField]
    private Text textTimeQuest;

    [SerializeField]
    private ItemManager itemManager; 

    public void Awake()
    {
        ins = this;
    }

    public void Start()
    {
        indexQuest = 0;
        CheckQuest(indexQuest);
    }

    public void CheckQuest(int _index)
    {
        if (_index == 0)
        {
            isQuest = false;
            InitStart();
        }
        else if (_index == 1)
        {
            isQuest = true;
            dataQuest = dataQuests[_index - 1];
            quest = listQuest[_index - 1];
            indexQuest = _index;
            enemyDie = dataQuest.indexEnemy;
            timeQuest = dataQuest.timeQuest;
            for (int i = 0; i < enemyDie; i++)
            {
                SpawnEnemy();
            }
            InitStart();

            TimeRunQuest();
        }
        else if (_index == 2)
        {
            isQuest = true;
            dataQuest = dataQuests[_index - 1];
            quest = listQuest[_index - 1];
            indexQuest = _index;
            enemyDie = dataQuest.indexEnemy;
            timeQuest = dataQuest.timeQuest;
            for (int i = 0; i < enemyDie; i++)
            {
                SpawnEnemy();
            }
            InitStart();
            TimeRunQuest();
        }
        else if (_index == 3)
        {
            isQuest = true;
            dataQuest = dataQuests[_index - 1];
            quest = listQuest[_index - 1];
            indexQuest = _index;
            pickupGive = dataQuest.indexPickup;
            timeQuest = dataQuest.timeQuest;
            CheckPointActiveFalse();        
            for (int i = 0; i < pickupGive; i++)
            {
                RandomPointPickup();
            }
            InitStart();
            TimeRunQuest();
        }
        else if (_index == 4)
        {
            isQuest = true;
            dataQuest = dataQuests[_index - 1];
            quest = listQuest[_index - 1];
            indexQuest = _index;
            pickupGive = dataQuest.indexPickup;
            timeQuest = dataQuest.timeQuest;
            CheckPointActiveFalse();
            for (int i = 0; i < pickupGive; i++)
            {
                RandomPointPickup();
            }
            InitStart();
            TimeRunQuest();

        }
        else if(_index == 5)
        {
            isQuest = true;
            dataQuest = dataQuests[_index - 1];
            quest = listQuest[_index - 1];
            indexQuest = _index;
            PoliceStarManager.ins.ChangeStarWanter(dataQuest.indexStar);
            CheckPointActiveFalse();
            for (int i = 0; i < dataQuest.indexPoint; i++)
            {
                RandomPointTaget();
            }

            timeQuest = dataQuest.timeQuest;
            InitStart();

            TimeRunQuest();
        }
        else if (_index == 6)
        {
            isQuest = true;
            dataQuest = dataQuests[_index - 1];
            quest = listQuest[_index - 1];
            indexQuest = _index;
            PoliceStarManager.ins.ChangeStarWanter(dataQuest.indexStar);
            CheckPointActiveFalse();
            for (int i = 0; i < dataQuest.indexPoint; i++)
            {
                RandomPointTaget();
            }
            timeQuest = dataQuest.timeQuest;
            InitStart();
            TimeRunQuest();
        }
        else if (_index == 7)
        {
            isQuest = true;
            dataQuest = dataQuests[_index - 1];
            quest = listQuest[_index - 1];
            indexQuest = _index;
            PoliceStarManager.ins.ChangeStarWanter(dataQuest.indexStar);
            CheckPointActiveFalse();
            for (int i = 0; i < dataQuest.indexPoint; i++)
            {
                RandomPointTaget();
            }

            timeQuest = dataQuest.timeQuest;
            InitStart();

            TimeRunQuest();
        }
        else if (_index == 8)
        {
            isQuest = true;
            dataQuest = dataQuests[_index - 1];
            quest = listQuest[_index - 1];
            indexQuest = _index;
            for (int i = 0; i < dataQuest.indexPoint; i++)
            {
                SpawnCar();
            }
            timeQuest = dataQuest.timeQuest;
            InitStart();
            TimeRunQuest();
        }
    }

    public void TimeRunQuest()
    {
        timeQuest -= 1;
        if (timeQuest <= 0)
        {
            LoseQuest();
        }
        else
        {
            Invoke("TimeRunQuest", 1f);
            
        }
        TextTime();
    }

    public void TextTime()
    {
        int minute = timeQuest/ 60;
        int second = timeQuest % 60;
        textTimeQuest.text = "0" + minute + ":" + second;
    }


    public void InitStart()
    {
        if (isQuest)
        {
            for (int i = 0; i < startQuestPoint.Count; i++)
            {
                startQuestPoint[i].gameObject.SetActive(false);
            }
            PoliceStarManager.ins.isQuest = true;
           
            textTimeQuest.enabled = true;
        }
        else
        {
            for (int i = 0; i < startQuestPoint.Count; i++)
            {
                startQuestPoint[i].gameObject.SetActive(true);
            }
            PoliceStarManager.ins.isQuest = false;
            textTimeQuest.enabled = false;
        }     
    }

    public void EnemyDie()
    {
        enemyDie -= 1;
        if (enemyDie == 0)
        {
            CompleteQuest();
        }
    }

    public void GivePickUp()
    {
        pickupGive -= 1;
        if (pickupGive <= 0)
        {
            CompleteQuest();
        }
    }

    public void ComplePoint()
    {
        pointQuest -= 1;
        if(pointQuest <= 0)
        {
            CompleteQuest();
        }
    }

    public void CarDestroy()
    {
        carDestroy -= 1;
        if(carDestroy <= 0)
        {
            CompleteQuest();
        }
    }

    public void CheckPointActiveFalse()
    {
        for (int i = 0; i < quest.checkPoint.Count; i++)
        {
            quest.checkPoint[i].gameObject.SetActive(false);
        }
    }

    public void RandomPointPickup()
    {
        int _index = Random.Range(0, quest.checkPoint.Count);
        if (quest.checkPoint[_index].gameObject.activeSelf)
        {
            RandomPointPickup();
        }
        else
        {
            quest.checkPoint[_index].gameObject.SetActive(true);
        }
    }

    public void RandomPointTaget()
    {
        int _index = Random.Range(0, quest.checkPoint.Count);
        if (quest.checkPoint[_index].gameObject.activeSelf)
        {
            RandomPointPickup();
        }
        else
        {
            quest.checkPoint[_index].gameObject.SetActive(true);
        }
    }

    public void SpawnEnemy()
    {
        int _index = Random.Range(0, quest.checkPoint.Count);
        NPCPooling.ins.SpawnNPCQuest(quest.checkPoint[_index].transform.position);    
    }

    public void SpawnCar()
    {
        int _index = Random.Range(0, quest.checkPoint.Count);
        NPCPooling.ins.SpawnCarQuest(quest.checkPoint[_index].transform.position);
    }

    public void CompleteQuest()
    {
        //PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + dataQuest.reward);
        itemManager.AddMoney(dataQuest.reward);
        indexQuest = 0;
        isQuest = false;
        dataQuest = null;
        PoliceStarManager.ins.ChangeStarWanter(0);
        NPCPooling.ins.CheckEndQuest();
        InitStart();    
    }

    public void LoseQuest()
    {
        indexQuest = 0;
        isQuest = false;
        dataQuest = null;
        NPCPooling.ins.CheckEndQuest();
        InitStart();
    }
}
