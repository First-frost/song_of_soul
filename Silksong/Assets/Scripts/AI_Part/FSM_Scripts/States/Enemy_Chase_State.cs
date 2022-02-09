using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chase_State :EnemyFSMBaseState
{
    public float chaseSpeed;
    public bool isFaceWithSpeed;
    public bool isFlying = false;
    public bool lock_x_move = false;
    public bool lock_y_move = false;
    private Vector3 v;
    public override void InitState(EnemyFSMManager enemyFSM)
    {
        base.InitState(enemyFSM);
        
    }
    public override void EnterState(EnemyFSMManager enemyFSM)
    {
        base.EnterState(enemyFSM);
        if (isFlying)
        {
            enemyFSM.rigidbody2d.gravityScale = 0;
        }
    }
    public override void Act_State(EnemyFSMManager fSM_Manager)
    {
        v = fSM_Manager.getTargetDir(true);
        v=v.normalized;
        if (!isFlying)
        {
            if (v.x > 0)
                v = new Vector3(1, 0, 0);
            else
                v = new Vector3(-1, 0, 0);
        }
        if (lock_x_move)
            v.x = 0;
        if (lock_y_move)
            v.y = 0;
        fSM_Manager.transform.Translate(v * chaseSpeed * Time.deltaTime);
        if (isFaceWithSpeed)
            fSM_Manager.faceWithSpeed();
    }


}
