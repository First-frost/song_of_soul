using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * �󽣹ֵĹ����߼����ڹ���������ʱ������һ���ѵ�Ч��
 * �ѵ�Ч��Ҳ���԰��ڹ���������ĳ���׶�
 * ��������û�ж���������������
 */
public class Enemy_BroadswordAttack_State : Enemy_Attack_State
{
    public GameObject crack;
    protected RaycastHit2D hit;
    public LayerMask layerMask;

    public override void ExitState(EnemyFSMManager enemyFSM)
    {
        base.ExitState(enemyFSM);
        fsmManager.rigidbody2d.velocity = Vector2.zero;
        hit = Physics2D.Raycast(enemyFSM.transform.position, -enemyFSM.transform.up, 10, layerMask);
        GameObject.Instantiate(crack).transform.position = hit.point;
    }
}
