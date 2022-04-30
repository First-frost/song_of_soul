using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ����trigger����Ҫ����Ϊ�����distanceToPlayer�Ǹ�trigger
 * �����ж���Ծ����ķ����ǹ����x-��ҵ�x
 * �����Ļ��������ڹ����ͷ������distanceToPlayer��
 * ����ͻ�������һֱ��������ȥ�ͺܴ�
 * ����д�����trigger������
 */
public class SightCheckTrigger : EnemyFSMBaseTrigger
{
    public float sightLength = 10;
    public float startLength = 0;
    public bool targetInRange=true;//Ϊtrue�Ǹ�trigger���Ŀ���Ƿ��ڷ�Χ�ڣ�Ϊfalseʱ���Ŀ���Ƿ��ڷ�Χ��
    public LayerMask layer;
    RaycastHit2D hit;
    Vector2 face;
    Vector2 pos;
    Vector2 box;
    public override void InitTrigger(EnemyFSMManager fsm_Manager)
    {
        base.InitTrigger(fsm_Manager);
        box=fsm_Manager.GetComponent<Collider2D>().bounds.size;
    }

    public override bool IsTriggerReachInFixUpdate(EnemyFSMManager fsm_Manager)
    {
        face = fsm_Manager.transform.right * fsm_Manager.transform.localScale.x;
        pos = fsm_Manager.GetComponent<Collider2D>().bounds.center;
        pos.y-=box.y/2;
        for (int i = 0; i < 10; i++)
        {
            hit = Physics2D.Raycast(pos + face.normalized * startLength, face.normalized, sightLength - startLength, layer);
            pos.y += box.y / 10;
            Debug.DrawLine(pos, pos + face.normalized * sightLength);
            if (hit.collider != null) return targetInRange;
        }        
        return !targetInRange;
    }
}
