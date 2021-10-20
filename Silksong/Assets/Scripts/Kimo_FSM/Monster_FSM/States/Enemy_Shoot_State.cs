using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot_State : EnemyFSMBaseState
{
    public GameObject bullet;
    //public float range;
    public float shotCD;
    public float distance;
    public float speed;
    private float time = 0;
    public Transform target;
    //public class MonoStub : MonoBehaviour { };

    public override void Act_State(FSMManager<EnemyStates, EnemyTriggers> fSM_Manager)
    {
        base.Act_State(fSM_Manager);
        time += Time.deltaTime;
        if (time >= shotCD)
        {
            Shot();
            time = 0;
        }
    }


    public override void EnterState(FSMManager<EnemyStates, EnemyTriggers> fSM_Manager)
    {
        base.EnterState(fSM_Manager);
        //if (shotCD > 0)
        //{
        //TimeCounter();
        //}
        //else Debug.Log("shot cd can not <=0");
    }
    public override void ExitState(FSMManager<EnemyStates, EnemyTriggers> fSM_Manager)
    {
        base.ExitState(fSM_Manager);
        //Debug.Log("����");
        //GameObject Emitter = GameObject.Find("Emitter");
        //if (Emitter != null)
        //{
        //     UnityEngine.Object.Destroy(Emitter);
        //}

    }


    public override void InitState(FSMManager<EnemyStates, EnemyTriggers> fSM_Manager)
    {
        base.InitState(fSM_Manager);
        fsmManager = fSM_Manager;
        stateID = EnemyStates.Enemy_Shoot_State;
    }
    /*
        public void TimeCounter()
        {
            GameObject Emitter = GameObject.Find("Emitter");
            if (Emitter == null)
            {
                Emitter = new GameObject();
                Emitter.name = "Emitter";
                Emitter.AddComponent<MonoStub>().StartCoroutine(ShotAcidLoop());
            }
            //Debug.Log("��ʼЭ��");
        }

        private IEnumerator ShotAcidLoop()
        {
            while (true)
            {
                //Debug.Log("׼������");
                yield return new WaitForSeconds(shotCD);//��shotcdΪ������Ϸ���
                ShotAcid();
            }
        }
    */
    private void Shot()
    {
        //Debug.Log("����");
        GameObject target = GameObject.FindWithTag("Player");
        Vector3 move = (target.transform.position - fsmManager.transform.position).normalized;
        //Debug.Log(move);
        //Collider2D target = Physics2D.OverlapCircle(transform.position, range, layer);
        GameObject shot = UnityEngine.Object.Instantiate(bullet);
        shot.transform.position = fsmManager.transform.position;
        shot.GetComponent<Rigidbody2D>().velocity = move * speed;
        UnityEngine.Object.Destroy(shot, distance);
    }

}
