using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ״̬���������Ļ���
/// </summary>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
public abstract class FSMManager<T1,T2> : MonoBehaviour
{

    public Animator animator;
    public AnimatorStateInfo currentStateInfo;
    public AudioSource audio;
    public Rigidbody2D rigidbody;

    public Collider2D triggerCollider;
    public Collision2D collision;

    /// /// <summary>
    /// ��ǰ״̬
    /// </summary>
    public FSMBaseState<T1,T2> currentState;
    [DisplayOnly]
    public T1 currentStateID;
    /// <summary>
    /// ����״̬
    /// </summary>
    public FSMBaseState<T1,T2> anyState;
    public T1 defaultStateID;
    /// <summary>
    /// ��ǰ״̬������������״̬�б�
    /// </summary>
    public Dictionary<T1, FSMBaseState<T1,T2>> statesDic = new Dictionary<T1, FSMBaseState<T1,T2>>();
    /// <summary>
    /// ����״̬�б����Ӧ�����б��SO�ļ�
    /// </summary>


    public void ChangeState(T1 state)
    {
        if (currentState != null) { }
            currentState.ExitState(this);
        if (statesDic.ContainsKey(state))
        {
            currentState = statesDic[state];
            currentStateID = state;
        }
        else
        {
            Debug.LogError("����״̬������");
        }
        currentState.EnterState(this);
        if (currentState.animName != null)
        {
            animator.Play(currentState.animName);
            currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            //Debug.LogWarning(currentStateInfo.normalizedTime);
            currentStateInfo = default;
            Debug.LogWarning(currentStateInfo.normalizedTime);
        }
    }

    //public FSMBaseState<T1,T2> AddState(T1 state)
    //{
    //    //Debug.Log(triggerID);

    //    Type type = Type.GetType("Enemy"+state + "State");
    //    if (type == null)
    //    {
    //        Debug.LogError(state + "�޷���ӵ�" + "��states�б�");
    //        Debug.LogError("���stateIDö��ֵ����Ӧ��������Ӧö�������ϡ�_State������ö��ֵΪIdle��״̬����ΪIdle_State���������ü��أ�");
    //        return null;
    //    }
    //    else
    //    {
    //        FSMBaseState<T1,T2> temp = Activator.CreateInstance(type) as FSMBaseState<T1,T2>;
    //        statesDic.Add(state,temp);
    //        return temp;
    //    }
    //}
    //public FSMBaseState<T1,T2> AddState(T1 state,FSMBaseState<T1,T2> stateClass)
    //{
    //    statesDic.Add(state, stateClass);
    //    return stateClass;
    //}
    //public void RemoveState(T1 state)
    //{
    //    if (statesDic.ContainsKey(state))
    //        statesDic.Remove(state);
    //}
    /// <summary>
    /// ���ڳ�ʼ��״̬���ķ������������״̬����������ӳ�����ȡ��������ȡ�Awakeʱִ�У��ɲ�ʹ�û��෽���ֶ��������
    /// </summary>
    /// 

    public virtual void InitWithScriptableObject()
    {
    }
    public virtual void InitManager()
    {
        InitWithScriptableObject();
        ////�����ȡ
        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
        if (GetComponent<AudioSource>() != null)
        {
            audio = GetComponent<AudioSource>();
        }
        if(GetComponent<Rigidbody2D>()!=null)
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

    }

    private void Awake()
    {
        statesDic.Clear();
        InitManager();

    }

    private void Start()
    {
        if (statesDic.Count == 0)
            return;
        //Ĭ��״̬����
        currentStateID = defaultStateID;
        ChangeState(currentStateID);
        if (anyState != null)
            anyState.EnterState(this);
        foreach (var state in statesDic.Values)
            foreach (var value in state.triggers)
            {
                Debug.LogWarning(this + "  " + state + "  " + value + "  " + value.GetHashCode());
            }
    }

    private void Update()
    {
        if (anyState != null)
        {
            anyState.Act_State(this);
            anyState.TriggerState(this);
        }
        if (currentState != null)
        {
            //ִ��״̬����
            currentState.Act_State(this);
            //���״̬�����б�
            currentState.TriggerState(this);
            currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }
        else
        {
            Debug.LogError("currentStateΪ��");
        }
    }

}

public abstract class FSMManagerInherit<T1, T2, T3, T4> : FSMManager<T1, T2>
{
    public List<State_SO_Config<T1, T2, T3, T4>> stateConfigs;
    public State_SO_Config<T1, T2, T3, T4> anyStateConfig;
    public override void InitWithScriptableObject()
    {
        if (anyStateConfig != null)
        {
            anyState = (FSMBaseState<T1, T2>)ObjectClone.CloneObject(anyStateConfig.stateConfig);
            anyState.triggers = new List<FSMBaseTrigger<T1, T2>>();
            for (int k = 0; k < anyStateConfig.triggerList.Count; k++)
            {
                anyState.triggers.Add(ObjectClone.CloneObject(anyStateConfig.triggerList[k]) as FSMBaseTrigger<T1, T2>);
                anyState.triggers[anyState.triggers.Count - 1].InitTrigger(this);
                //Debug.Log(this.gameObject.name+"  "+ anyState.triggers[anyState.triggers.Count - 1]+"  "+anyState.triggers[anyState.triggers.Count - 1].GetHashCode());
            }
            anyState.InitState(this);
        }
        for (int i = 0; i < stateConfigs.Count; i++)
        {
            FSMBaseState<T1, T2> tem = ObjectClone.CloneObject(stateConfigs[i].stateConfig) as FSMBaseState<T1, T2>;
            tem.triggers = new List<FSMBaseTrigger<T1, T2>>();
            for (int k = 0; k < stateConfigs[i].triggerList.Count; k++)
            {
                tem.triggers.Add(ObjectClone.CloneObject(stateConfigs[i].triggerList[k]) as FSMBaseTrigger<T1, T2>);
                tem.triggers[tem.triggers.Count - 1].InitTrigger(this);
                //Debug.Log(this.gameObject.name + "  " + tem.triggers[tem.triggers.Count - 1] + "  " + tem.triggers[tem.triggers.Count - 1].GetHashCode());
            }
            statesDic.Add(stateConfigs[i].stateID, tem);
            tem.InitState(this);
        }
    }
}

/// <summary>
///����Enemy״̬������������Ϊ�����SO���ù���
/// </summary>
public class EnemyFSMManager : FSMManagerInherit<EnemyStates, EnemyTriggers, EnemyFSMBaseState, EnemyFSMBaseTrigger> { }
/// <summary>
///����NPC״̬������������Ϊ�����SO���ù���
/// </summary>
public class NPCFSMManager : FSMManagerInherit<NPCStates, NPCTriggers, NPCFSMBaseState, NPCFSMBaseTrigger> { }
/// <summary>
/// ����Player״̬����������Ĭ��û�����SO���ù��ܣ�
/// ����Ҫ��
/// ����ȡ���������ע��
/// Ȼ���Player_State_SO_Config�ű���ȡ������Player_State_SO_Config���ע�ͼ��ɡ�
/// 
/// </summary>
public class PlayerFSMManager : FSMManagerInherit<PlayerStates, PlayerTriggers, PlayerFSMBaseState, PlayerFSMBaseTrigger> { }

