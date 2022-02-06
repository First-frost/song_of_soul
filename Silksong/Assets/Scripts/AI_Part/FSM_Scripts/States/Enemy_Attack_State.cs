using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_State : EnemyFSMBaseState
{

    public bool isFaceToTarget;
    public override void EnterState(EnemyFSMManager fSM_Manager)
    {
        if (isFaceToTarget)
            fSM_Manager.getTargetDir(true);
        base.EnterState(fSM_Manager);
        fSM_Manager.rigidbody2d.velocity = Vector2.zero;
    }

    public override void InitState(EnemyFSMManager fSMManager)
    {
        base.InitState(fSMManager);
        fsmManager = fSMManager;
    }

}
