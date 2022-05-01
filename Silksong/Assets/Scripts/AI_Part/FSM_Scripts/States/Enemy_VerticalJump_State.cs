using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ��ֱ��Ծ�ֵ���Ծ״̬
 * �����������һ�����ƶ�ʱ�����Ծ
 * �ڶ����ǹ���ʱ�ᷴ��������������ӵ���
 */
public class Enemy_VerticalJump_State : EnemyFSMBaseState
{
    public float jumpSpeed;//��Ծ������
    public string shootType="normal";
    public bool ifAttack;//�Ƿ��ǹ�����Ծ״̬
    private ShootSystem ShotPoint;
    //public GameObject bullet;//������Ծʱ������ӵ�
    public LayerMask layer;
    Vector2 velocity;//�����õ�����Ծ�ٶ�
    Vector2 jumpDirect;//��Ծ����
    Vector2 startPos;//������λ��,��ʵ���ǵ�ǰλ��
    float baseDirect;//һ��ʼ��Ŀ������ľ��룬��������ֵ��һ��ʱ�������·�ת
    bool ifFlip;//�Ѿ���ת���Ͳ����ٷ�ת��
    public override void InitState(EnemyFSMManager enemyFSM)
    {
        base.InitState(enemyFSM);
        ShotPoint = enemyFSM.gameObject.transform.GetChild(1).gameObject.GetComponent<ShootSystem>();
    }
    public override void EnterState(EnemyFSMManager enemyFSM)
    {
        jumpDirect = CheckPlatformTrigger.jumpDirectDic[enemyFSM];
        ifFlip = false;
        startPos=enemyFSM.transform.position;
        baseDirect = jumpDirect.sqrMagnitude;
        velocity = jumpDirect.normalized * jumpSpeed;
        enemyFSM.rigidbody2d.velocity = velocity;
        base.EnterState(enemyFSM);
    }
    public override void Act_State(EnemyFSMManager enemyFSM)
    {
        base.Act_State(enemyFSM);
        if (!ifFlip)
        {
            float currentDir = ((Vector2)enemyFSM.transform.position - startPos).sqrMagnitude;
            if (currentDir >= baseDirect / 3)//��������֮һλ��ʱ��ת������ǹ�����Ծ������ӵ�
            {
                enemyFSM.transform.up = -enemyFSM.transform.up;
                ifFlip =true;
                if(ifAttack)ShotPoint.Shoot(shootType);
                //enemyFSM.transform.localScale = scale;
            }
        }
    }
    public override void ExitState(EnemyFSMManager enemyFSM)
    {
        base.ExitState(enemyFSM);
        enemyFSM.rigidbody2d.velocity = Vector2.zero;
    }
}
