using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ��½��Trigger
 * ûʲô�ر�ģ���Ҫ�����ö����������ת��״̬
 * ����û�и���Trigger�����������������һ��
*/
public class LandTrigger : EnemyFSMBaseTrigger
{
    public LayerMask layerMask;
    public bool fall;
    Collider2D collider;
    //float preSpeedY=1;
    //float times = 0;
    public override void InitTrigger(EnemyFSMManager fsm_Manager)
    {
        base.InitTrigger(fsm_Manager);
        collider = fsm_Manager.GetComponent<Collider2D>();
    }
    public override bool IsTriggerReachInFixUpdate(EnemyFSMManager enemyFSM)
    {
        float speedY = Vector3.Project(enemyFSM.rigidbody2d.velocity,enemyFSM.transform.up).sqrMagnitude;
        if (collider.IsTouchingLayers(layerMask) && speedY <= 0.1)
        {
            enemyFSM.rigidbody2d.velocity = Vector2.zero;
            if(enemyFSM.animator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.99)
                return true;
        }
        return false;
    }
}
