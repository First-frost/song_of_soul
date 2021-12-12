using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System;
//using DG.Tweening;

public class TalkEvent : MonoBehaviour
{
    public GameObject NPCTalkPanel;
    public Text NPCText;
    public Text NPCName;


    public List<int> TalkFlags = new List<int>();//-1�������ģ�����0�����ִ������Ի���������һ�ξ���Ի��ͷ���-1,1�������εĻ��ټ�һ��2�������ξ���Ի�����[-1,1,2,3]��˳�򴥷����Ի������Ѷ�Ӧ��flag����-1����ֱ��ɾ��
    public List<int> StartTalkNo = new List<int>();//�����������ɫ��ÿ�ζԻ��ĵ�һ�仰�ı�ŷŽ����List������Ի���TalkFlags��Ԫ�ض�Ӧ�����ĶԻ����ڼ���Ԫ��֮�����ѡ��

    public IfTalkCondition[] ifTalkConditions;
    public Dictionary<int, int[]> ConditionNos = new Dictionary<int, int[]>(); //ǰ����TalkFlags�ĶԻ���ţ����ڶ�λConditionID,������ConditionNo����λ��Condition��


    public List<int> Condition = new List<int>();//�ж������Ƿ�������б�


    [HideInInspector]
    public int ID = 0;//���������ǵ�һ�仰�����Ѿ��Ǻ���ĶԻ���
    [HideInInspector]
    public int count = 0;



    void Start()
    {

        foreach (IfTalkCondition item in ifTalkConditions)
        {
            ConditionNos.Add(item.ConditionID, item.ConditionNo);
        }

    }

    void StartTalk()
    {
        for (int num = 0; num < TalkFlags.Count; num++) //�ж��Ƿ���������
        {
            if (TalkFlags[num] != -1) //�������-1������Ի��������-1��˵����ǰnum��Ӧ�ĶԻ��Ѿ��Ի�����
            {
                for (int j = 0; j < ConditionNos[num].Length; j++) //�жϿ�������Ի��������Ƿ�ȫ��Ϊ1
                {
                    if (Condition[j] != 1) //�����һ��������Ϊ1��˵����һ������Ի������ܴ���
                    {
                        break;
                    }
                    else
                    {
                        count += 1;
                    }
                }
                if (count == ConditionNos[num].Length)
                {
                    NPCTalkPanel.SetActive(true);
                    TalkFlags[num] = -1;
                    ID = StartTalkNo[num];
                    NPCText.text = TalkManager.Instance.TalkContent[ID];
                    NPCName.text = TalkManager.Instance.TalkNPC[ID];
                }
                count = 0;
            }
        }
    }

    void Next()
    {
        //ID = StartTalkNo[0];
        if (Input.GetMouseButtonDown(0))
        {
            if (TalkManager.Instance.TalkNo[ID] != -1)//���NextID��Ϊ0
            {
                ID = TalkManager.Instance.TalkNo[ID];//ID����NextID
                NPCText.text = TalkManager.Instance.TalkContent[ID];//�����ı�
                NPCName.text = TalkManager.Instance.TalkNPC[ID]; //����˵����
            }
            else
            {
                NPCTalkPanel.SetActive(false);
                ID = 0;
            }
        } 
    }


    // Update is called once per frame
    void Update()
    {
        if (ID == 0)
        {
            StartTalk();
        }
        else
        {
            Next();
        }
    }
}