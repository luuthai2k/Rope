using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPoint : MonoBehaviour
{

    [SerializeField]
    private bool isStarQuest;

    [SerializeField]
    private int indexQuest;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (isStarQuest)
            {
                QuestManager.ins.CheckQuest(indexQuest);
            }

            else
            {
                if(indexQuest == 3)
                {
                    QuestManager.ins.GivePickUp();
                }
                else if(indexQuest == 4)
                {
                    QuestManager.ins.GivePickUp();
                }
                else if(indexQuest == 5)
                {
                    QuestManager.ins.ComplePoint();
                }
                else if(indexQuest == 6)
                {
                    QuestManager.ins.ComplePoint();
                }
                else if(indexQuest == 7)
                {
                    QuestManager.ins.ComplePoint();
                }
            }
        }

    }
}
