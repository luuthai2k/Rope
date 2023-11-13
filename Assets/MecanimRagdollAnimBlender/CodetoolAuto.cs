using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class CodetoolAuto : MonoBehaviour
{
    [SerializeField]
    private List<PhysicsBodyPart> physicsBodyParts1, physicsBodyParts2, physicsBodyParts;

    [SerializeField]
    private LevelUpgradeUi level;

    [SerializeField]
    private Button button;

    [SerializeField]
    private List<PointAIMove> point;

    [SerializeField]
    private List<Transform> line;

    [SerializeField]
    private Color color;

    [ContextMenu("check")]
    public void Check()
    {
        //for (int i = 0; i < physicsBodyParts1.Count; i++)
        //{
        //    physicsBodyParts1[i].m_blend = physicsBodyParts2[i].m_blend;
        //    physicsBodyParts1[i].m_animBone = physicsBodyParts2[i].m_animBone;
        //    physicsBodyParts1[i].m_animator = physicsBodyParts2[i].m_animator;
        //    physicsBodyParts1[i].m_connectedParts = physicsBodyParts2[i].m_connectedParts;
        //    physicsBodyParts1[i].m_controlRegain = physicsBodyParts2[i].m_controlRegain;
        //    physicsBodyParts1[i].m_defaultControlRegain = physicsBodyParts2[i].m_defaultControlRegain;
        //    physicsBodyParts1[i].m_connectedPartsWeight = physicsBodyParts2[i].m_connectedPartsWeight;
        //    physicsBodyParts1[i].m_balanceRatio = physicsBodyParts2[i].m_balanceRatio;
        //    physicsBodyParts1[i].m_blendMax = physicsBodyParts2[i].m_blendMax;
        //    physicsBodyParts1[i].m_rigidness = physicsBodyParts2[i].m_rigidness;
        //    physicsBodyParts1[i].m_blendSpeed = physicsBodyParts2[i].m_blendSpeed;
        //    physicsBodyParts1[i].mBounceFactor = physicsBodyParts2[i].mBounceFactor;

        //}

        //for (int i = 0; i < physicsBodyParts.Count; i++)
        //{
        //    if (physicsBodyParts[i].m_animator != null)
        //    {
        //        physicsBodyParts2.Add(physicsBodyParts[i]);
        //    }
        //    else
        //    {
        //        physicsBodyParts1.Add(physicsBodyParts[i]);
        //    }
        //}
        //level = GetComponent<LevelUpgradeUi>();

        //level.Add(transform.GetChild(0).GetComponent<Image>(), transform.GetChild(1).GetComponent<Image>());

        //button = gameObject.GetComponent<Button>();


        //for (int i = 0; i < point.Count; i++)
        //{
        //    for (int j = 0; j < point.Count; j++)
        //    {
        //        if (Mathf.Abs(point[i].transform.position.x - point[j].transform.position.x) < 1 || Mathf.Abs(point[i].transform.position.z - point[j].transform.position.z) < 1)
        //        {
        //            Debug.LogError("check");
        //            if (i != j)
        //            {
        //                Debug.LogError(point[i]);
        //                point[i].nextpoint.Add(point[j].transform);
        //            }

        //        }
        //    }
        //}


        //Debug.LogError(transform.position);
        
        for (int i = 0; i < line.Count -1; i++)
        {
       
            line[i].GetComponent<PointAIMove>().nextpoint.Add(line[i + 1]);
        }
    }

    public void Update()
    {
        Transform[] ts = gameObject.GetComponentsInChildren<Transform>();

       
        for (int i = 0; i < ts.Length-1; i++)
        {
            Debug.DrawLine(ts[i].position, ts[i + 1].position,color);
        }
        
    }
}
