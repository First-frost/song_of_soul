using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * һ��ʼ�����Լ�д�ڶ��߼��ģ�����д�����淢���ܻ�ƫ����
 * ���ǿ�������ڹ�����˶����ֻ���������ʧ�ٶȣ������¾�ͣ��
 * ����ý������������ǽ���������������ʱ���˲�ƣ�����Ч���ϻ��Ǻܲ���ıϾ����Դ���
 * ���������Ƭ�����ƵĶ��ܽ��Ļ�˲�ƾͻ��Ƶ��
 * �Ǿͱ��ý����ˣ��͸��ô��룬������ע�ʹ��룬�ȳ�������������Ч��
 */
public class Enemy_HookToMoonPoint_State : EnemyFSMBaseState
{
    public float minDisToPoint;//�ڶ��뾶
    BreakMoonPoint targetPoint;//����ת���Ǹ�������Ƭ
    float baseG;
    HingeJoint2D hingeJoint;
    //public float rotateSpeed;
    //float centripetalF;
    //float power;
    //float baseH;
    //Vector2 rotateVelocity;
    //Vector2 dir;
    public override void InitState(EnemyFSMManager enemyFSM)
    {
        hingeJoint=enemyFSM.transform.GetComponent<HingeJoint2D>();
        base.InitState(enemyFSM);
    }
    public override void EnterState(EnemyFSMManager enemyFSM)
    {
        hingeJoint.enabled=true;
        targetPoint = FindMoonPoinTrigger.moonPointDic[enemyFSM];
        hingeJoint.connectedAnchor = targetPoint.transform.position;
        baseG = enemyFSM.rigidbody2d.gravityScale;
        //baseH = enemyFSM.transform.position.y;
        //power =enemyFSM.rigidbody2d.velocity.sqrMagnitude/2;
        //enemyFSM.rigidbody2d.velocity = Vector2.zero;
        //Rotate(enemyFSM);
        targetPoint.atBreakMoonPoint();
        base.EnterState(enemyFSM);
    }
    //public void Rotate(EnemyFSMManager enemyFSM)
    //{
    //    enemyFSM.StartCoroutine(Rotating(enemyFSM));
    //    //enemyFSM.StartCoroutine(CircleRotating(enemyFSM));
    //}
    //public IEnumerator Rotating(EnemyFSMManager enemyFSM)
    //{       
    //    while (true)
    //    {
    //        dir = (targetPoint.transform.position - enemyFSM.transform.position).normalized;       
    //        rotateSpeed = enemyFSM.rigidbody2d.velocity.sqrMagnitude;
    //        centripetalF = enemyFSM.rigidbody2d.mass * rotateSpeed  / minDisToPoint;         
    //        Vector2 G = -Vector3.Project(Physics2D.gravity * baseG, dir);
    //        //float h = Mathf.Abs(targetPoint.transform.position.y - baseH);
    //        //float v = Mathf.Sqrt(Mathf.Abs(2 * Physics2D.gravity.y * baseG * h));
    //        //enemyFSM.rigidbody2d.velocity = enemyFSM.rigidbody2d.velocity.normalized * v;
    //        if (enemyFSM.transform.position.y < targetPoint.transform.position.y)
    //        {
    //            enemyFSM.rigidbody2d.AddForce(G+centripetalF * dir);
    //            Debug.DrawLine(enemyFSM.transform.position, (Vector2)enemyFSM.transform.position+ G + centripetalF * dir- baseG * Physics2D.gravity);
    //            //Debug.DrawLine(enemyFSM.transform.position, (Vector2)enemyFSM.transform.position + baseG * Physics2D.gravity);
    //            Debug.DrawLine(enemyFSM.transform.position, (Vector2)enemyFSM.transform.position + enemyFSM.rigidbody2d.velocity, Color.red);
    //            enemyFSM.transform.position = (Vector2)targetPoint.transform.position - dir * minDisToPoint;
    //        }
    //        yield return new WaitForFixedUpdate();
    //    }
    //}
    //IEnumerator CircleRotating(EnemyFSMManager enemyFSM)
    //{
    //    while (ifRotating)
    //    {
    //        dir = (targetPoint.transform.position - enemyFSM.transform.position).normalized;
    //        enemyFSM.rigidbody2d.AddForce(dir *centripetalF);
    //        enemyFSM.rigidbody2d.AddForce(-baseG * Physics2D.gravity);
    //        rotateVelocity.x = dir.y;
    //        rotateVelocity.y = -dir.x;
    //        enemyFSM.rigidbody2d.velocity = rotateVelocity.normalized * rotateSpeed;
    //        yield return new WaitForFixedUpdate();
    //    }
    //}

    public override void ExitState(EnemyFSMManager enemyFSM)
    {
        //Ҫ������һ��������Ƭǰ�ȼ���
        float h = Mathf.Abs(targetPoint.transform.position.y - enemyFSM.transform.position.y);
        float v = Mathf.Sqrt(Mathf.Abs(2*Physics2D.gravity.y * baseG * h));
        enemyFSM.rigidbody2d.velocity = enemyFSM.rigidbody2d.velocity.normalized * v*1.4f;
        FindMoonPoinTrigger.preMoonPointDic[enemyFSM] = targetPoint;
        base.ExitState(enemyFSM);
    }
}
