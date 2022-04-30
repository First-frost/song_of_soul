using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �����������Ƿ���һ��������Ƭ�ķ�Χ��
 * minDistance��ñ�������Ƭ�İڶ��뾶Ҫ��һ��
 */
public class DistanceToMoonPointTrigger: EnemyFSMBaseTrigger
{
    public float minDistance;
    float sqrMinDistance;
    public override void InitTrigger(EnemyFSMManager enemyFSM)
    {
        sqrMinDistance = minDistance * minDistance;
        base.InitTrigger(enemyFSM);
    }
    public override bool IsTriggerReachInFixUpdate(EnemyFSMManager enemyFSM)
    {
        if((enemyFSM.transform.position - FindMoonPoinTrigger.moonPointDic[enemyFSM].transform.position).sqrMagnitude > sqrMinDistance
            ||enemyFSM.rigidbody2d.velocity.magnitude>1)
        {
            return false;
        }return true;
    }
}
