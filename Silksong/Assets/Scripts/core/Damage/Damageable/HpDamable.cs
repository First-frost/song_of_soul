using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using System;
using UnityEngine.Events;
/// <summary>
/// 
/// ӵ������ֵ����ط�����damable 
/// </summary>���ߣ����
public class HpDamable :Damable
{
    [SerializeField]
    private int maxHp ;//�������ֵ
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }

    [SerializeField]
    private int currentHp;//��ǰhp
    public int CurrentHp
    {
        get { return currentHp; }
    }
    

    public bool resetHealthOnSceneReload;

    [Serializable]
    public class dieEvent : UnityEvent<DamagerBase, DamageableBase>
    { }

    //[Serializable]
    public class setHpEvent : UnityEvent<HpDamable>
    { }

    public dieEvent onDieEvent;

    public setHpEvent onHpChange=new setHpEvent();

    public AudioCue dieAudio;//��audiomanager���а󶨹���hpdamableĬ�ϡ��ܻ�����Ч��Ч��
=======
/// <summary>
/// ���ߣ����
/// ӵ������ֵ����ط�����damable 
/// </summary>
public class HpDamable :Damable
{
    public int maxHp = 5;//�������ֵ

    public int currentHp;//��ǰhp

    public bool resetHealthOnSceneReload;

    protected Vector2 damageDirection;//�˺���Դ�ķ���

    public bool canHitBack;//�ܷ񱻻��� Ŀǰ���˻�����ʱ������ 
    public float hitBackDistance;
>>>>>>> 30f6fd9d (damage test)


    public override void takeDamage(DamagerBase damager)
    {
        if ( currentHp <= 0)
        {
<<<<<<< HEAD
           // return;
        }

        base.takeDamage(damager);
        addHp(-damager.getDamage(this),damager);

    }


    public void setCurrentHp(int hp,DamagerBase damager=null)
    {
        currentHp = Mathf.Clamp(hp,0,MaxHp);
        onHpChange.Invoke(this);
        if (currentHp == 0)
        {
            die(damager);
        }
    }

    public void addHp(int number,DamagerBase damager)//���ܵ��˺� number<0
    {
        setCurrentHp(currentHp + number,damager);
    }

    protected virtual void die(DamagerBase damager)
    {
        onDieEvent.Invoke(damager,this);

        if(gameObject.tag!="Player")//reborn player for  test
        Destroy(gameObject);//δ����

        Debug.Log(gameObject.name+" die");
        if (dieAudio)
        {
            dieAudio.PlayAudioCue();
        }

=======
            return;
        }

        base.takeDamage(damager);

        addHp(-damager.getDamage(this));
        damageDirection = damager.transform.position - transform.position;

        if(canHitBack)
        hitBack();
    }

    protected void hitBack()
    {

        if(damageDirection.x>0)
        {
            transform.Translate(Vector2.left *hitBackDistance,Space.World);
        }
        else
        {
            transform.Translate(Vector2.right * hitBackDistance, Space.World);
        }
    }
    public void setHp(int hp)
    {
        currentHp = hp;
        checkHp();
    }

    protected virtual void checkHp()
    {
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        if (currentHp <= 0)
        {
            die();
        }
    }
    protected void addHp(int number)//���ܵ��˺� number<0
    {
        currentHp += number;
        checkHp();

    }

    protected virtual void die()
    {
        Destroy(gameObject);//δ����
        Debug.Log(gameObject.name+" die");
>>>>>>> 30f6fd9d (damage test)
    }



}
