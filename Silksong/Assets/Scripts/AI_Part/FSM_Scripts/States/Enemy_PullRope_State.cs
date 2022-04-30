using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ������������һ��������Ƭ��״̬
 * һ��ʼ����ԭ�����Ǹ�������Ƭ������
 * Ȼ�������е�λ�û�����ߵ��λ�ã�����ѡ��Ŀǰ�õ������У�����
 * �������Ŀ��������Ƭ
 */
public class Enemy_PullRope_State : EnemyFSMBaseState
{
    public float MaxRopeForce=10;//����������Ƭ���ٶ�
    Vector2 targetPos;
    Vector2 dir;
    bool toTarget;
    float baseLength=1;
    public override void EnterState(EnemyFSMManager enemyFSM)
    {
        
        if (FindMoonPoinTrigger.preMoonPointDic[enemyFSM]==null)
        {
            baseLength = 1;
            targetPos = FindMoonPoinTrigger.moonPointDic[enemyFSM].transform.position;
            enemyFSM.StopAllCoroutines();
            toTarget = true;
        }
        else
        {
            enemyFSM.StartCoroutine( WaitToPullRope(enemyFSM));
        }           
        base.EnterState(enemyFSM);
    }
    public override void FixAct_State(EnemyFSMManager enemyFSM)
    {      
        if (toTarget)
        {
            Debug.DrawLine(enemyFSM.transform.position, (Vector2)enemyFSM.transform.position + enemyFSM.rigidbody2d.velocity, Color.red);
            enemyFSM.StopAllCoroutines();
            dir = (targetPos - (Vector2)enemyFSM.transform.position);
            Debug.DrawLine(enemyFSM.transform.position, dir + (Vector2)enemyFSM.transform.position);
            //enemyFSM.rigidbody2d.velocity = (dir.magnitude/baseLength) * MaxRopeForce*dir.normalized;
            //enemyFSM.rigidbody2d.velocity =MaxRopeForce * dir.normalized;
            enemyFSM.rigidbody2d.velocity = MaxRopeForce * dir;
            enemyFSM.rigidbody2d.AddForce(-Physics2D.gravity * enemyFSM.rigidbody2d.gravityScale);
            
        }
    }
    public override void ExitState(EnemyFSMManager enemyFSM)
    {
        toTarget = false;
        enemyFSM.rigidbody2d.freezeRotation = false;
        enemyFSM.StopAllCoroutines();
        base.ExitState(enemyFSM);
    }
   
    IEnumerator WaitToPullRope(EnemyFSMManager enemyFSM)
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        dir = (FindMoonPoinTrigger.moonPointDic[enemyFSM].transform.position - FindMoonPoinTrigger.preMoonPointDic[enemyFSM].transform.position).normalized;
        float Angle= Vector2.Angle(enemyFSM.transform.position - FindMoonPoinTrigger.moonPointDic[enemyFSM].transform.position, dir);
        float times = 0;//times��֤�ܵ�һ�������ȥ
        while ((Mathf.Abs(Angle-90)>10||Vector2.Dot(dir,enemyFSM.rigidbody2d.velocity)<0)||times<1f)
        {
            times += Time.fixedDeltaTime;
            Angle = Vector2.Angle(enemyFSM.transform.position- FindMoonPoinTrigger.preMoonPointDic[enemyFSM].transform.position, dir);
            yield return waitForFixedUpdate;
        }       
        enemyFSM.transform.GetComponent<HingeJoint2D>().enabled = false;      
        targetPos = enemyFSM.transform.position - FindMoonPoinTrigger.preMoonPointDic[enemyFSM].transform.position + FindMoonPoinTrigger.moonPointDic[enemyFSM].transform.position;
        enemyFSM.transform.up = ((Vector2)(FindMoonPoinTrigger.preMoonPointDic[enemyFSM].transform.position-enemyFSM.transform.position )).normalized;
        enemyFSM.rigidbody2d.freezeRotation = true;
        baseLength = (targetPos - (Vector2)enemyFSM.transform.position).magnitude;
        toTarget = true;
    }
    //IEnumerator WaitToPullRope2(EnemyFSMManager enemyFSM)
    //{
    //    yield return new WaitForFixedUpdate();  
    //    dir = (FindMoonPoinTrigger.moonPointDic[enemyFSM].transform.position - enemyFSM.transform.position);
    //    Vector2 baseLengt = (FindMoonPoinTrigger.moonPointDic[enemyFSM].transform.position - FindMoonPoinTrigger.preMoonPointDic[enemyFSM].transform.position);
    //    Vector2 preDir= (FindMoonPoinTrigger.preMoonPointDic[enemyFSM].transform.position - enemyFSM.transform.position);
    //    while (enemyFSM.rigidbody2d.velocity.magnitude>1f||Vector2.Dot(preDir,baseLengt)>0)
    //    {
    //        //baseLengt = (FindMoonPoinTrigger.moonPointDic[enemyFSM].transform.position - FindMoonPoinTrigger.preMoonPointDic[enemyFSM].transform.position).magnitude;
    //        Debug.Log(enemyFSM.rigidbody2d.velocity.magnitude > 0.5f);
    //        Debug.Log(Vector2.Dot(preDir, baseLengt) > 0);
    //        dir = (FindMoonPoinTrigger.moonPointDic[enemyFSM].transform.position - enemyFSM.transform.position);
    //        baseLengt = (FindMoonPoinTrigger.moonPointDic[enemyFSM].transform.position - FindMoonPoinTrigger.preMoonPointDic[enemyFSM].transform.position);
    //        preDir = (FindMoonPoinTrigger.preMoonPointDic[enemyFSM].transform.position - enemyFSM.transform.position);
    //        yield return new WaitForFixedUpdate();
    //    }
    //    enemyFSM.StopAllCoroutines();
    //    enemyFSM.StartCoroutine(PullRope(enemyFSM));
    //}

    //IEnumerator PullRope(EnemyFSMManager enemyFSM)
    //{
    //    toTarget = true;

    //    while (toTarget)
    //    {
    //        dir = (targetPos - (Vector2)enemyFSM.transform.position);
    //        Debug.DrawLine(enemyFSM.transform.position,dir+(Vector2)enemyFSM.transform.position);
    //        //enemyFSM.rigidbody2d.AddForce(dir * MaxRopeForce);
    //        enemyFSM.rigidbody2d.velocity = dir * MaxRopeForce;
    //        yield return new WaitForFixedUpdate();
    //    }
    //    Debug.Log(toTarget);
    //}
}
