using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Ѱ����һ��������Ƭ��trigger
 * ������Ѱ�ҷ�ʽһ�����������һ���ǳ�����ҷ���
 */
public class FindMoonPoinTrigger : EnemyFSMBaseTrigger
{
    BreakMoonPoint[] breakMoonPoints;//���е�������Ƭ����
    //moonPointDic��������Ӧ��Ҫ�������һ��������Ƭ���þ�̬�ֵ������HookToMoonPoint_Stateֱ���ҵ�Ҫ������һ��������Ƭ
    public static Dictionary<EnemyFSMManager, BreakMoonPoint> moonPointDic;
    // preMoonPointDicͬ�ϣ����Ǳ������ǰһ��������Ƭ
    public static Dictionary<EnemyFSMManager, BreakMoonPoint> preMoonPointDic;
    //NearMoonPoints�������һ��������Ƭ��Ӧ����Χ������������Ƭ��Ҫ����һ��������Ƭ��λ�þ��ǿ����
    public Dictionary<Transform,List<BreakMoonPoint>> NearMoonPoints;
    //�����ܴ�һ��������Ƭ������һ��������Ƭ�ķ�Χ���������Χ�ڵ�����������Ƭ��������
    public float checkRadius;
    //����ľ��䷶Χ�������ҽ��������Χ������Ѱ����һ��������Ƭ���߼������Ѱ�ұ��Ѱ��������������Ƭ
    public float alertRadius;
    float sqrCheckRadius;
    BreakMoonPoint target;//Ҫ�����������Ƭ

    public override void InitTrigger(EnemyFSMManager fsm_Manager)
    {
        if(moonPointDic==null) moonPointDic = new Dictionary<EnemyFSMManager, BreakMoonPoint>();
        if(preMoonPointDic==null) preMoonPointDic = new Dictionary<EnemyFSMManager, BreakMoonPoint>();
        NearMoonPoints=new Dictionary<Transform, List<BreakMoonPoint>>();
        if (!moonPointDic.ContainsKey(fsm_Manager))moonPointDic.Add(fsm_Manager,null);
        if (!preMoonPointDic.ContainsKey(fsm_Manager)) preMoonPointDic.Add(fsm_Manager, null);
        breakMoonPoints = GameObject.FindObjectsOfType<BreakMoonPoint>();
        sqrCheckRadius = checkRadius * checkRadius;
        for(int i = 0; i < breakMoonPoints.Length; i++)
        {
            NearMoonPoints.Add(breakMoonPoints[i].transform, new List<BreakMoonPoint>());
            for(int j = 0; j < breakMoonPoints.Length; j++)
            {
                if (i != j && (breakMoonPoints[i].transform.position - breakMoonPoints[j].transform.position).sqrMagnitude <= sqrCheckRadius)
                {
                    NearMoonPoints[breakMoonPoints[i].transform].Add(breakMoonPoints[j]);
                }
            }
        }
        NearMoonPoints.Add(fsm_Manager.transform, new List<BreakMoonPoint>());
        for (int j = 0; j < breakMoonPoints.Length; j++)
        {           
            if ((fsm_Manager.transform.position - breakMoonPoints[j].transform.position).sqrMagnitude <= sqrCheckRadius)
            {
                NearMoonPoints[fsm_Manager.transform].Add(breakMoonPoints[j]);
            }
        }       
        base.InitTrigger(fsm_Manager);
    }
    public BreakMoonPoint RandPoint(EnemyFSMManager enemyFSM)
    {
        if (preMoonPointDic[enemyFSM] != null)
        {
            int length = NearMoonPoints[preMoonPointDic[enemyFSM].transform].Count;
            return NearMoonPoints[preMoonPointDic[enemyFSM].transform][Random.Range(0, length)];
        }
        else
        {
            int length = NearMoonPoints[enemyFSM.transform].Count;
            return NearMoonPoints[enemyFSM.transform][Random.Range(0, length)];
        }
    }
    public BreakMoonPoint ClosestPoint(EnemyFSMManager enemyFSM)
    {
        Vector2 playerPos=(Vector2)enemyFSM.transform.position+enemyFSM.getTargetDir();
        float minDis = 10000;
        BreakMoonPoint ans=null;
        foreach(var point in NearMoonPoints[preMoonPointDic[enemyFSM].transform])
        {
            float dis = ((Vector2)point.transform.position - playerPos).magnitude;
            if (dis < minDis)
            {
                minDis = dis;
                ans = point;
            }
        }
        return ans;
    }
    public override bool IsTriggerReachInFixUpdate(EnemyFSMManager enemyFSM)
    {
        if (target == null)
        {
            if (enemyFSM.getTargetDir().magnitude < alertRadius) target=ClosestPoint(enemyFSM); 
            else target=RandPoint(enemyFSM);
            moonPointDic[enemyFSM] = target;
            target.bePicked();
        }
        if (target != null )
        {
            target = null;            
            return true;
        }
        return false;
    }
}
