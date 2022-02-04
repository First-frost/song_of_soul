using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
/// <summary>
/// 
/// 拥有生命值及相关方法的damable 
/// </summary>作者：青瓜
public class HpDamable :Damable
{
    public int maxHp ;//最大生命值

    public int currentHp;//当前hp

    public bool resetHealthOnSceneReload;

    [Serializable]
    public class dieEvent : UnityEvent<DamagerBase, DamageableBase>
    { }

    //[Serializable]
    public class setHpEvent : UnityEvent<HpDamable>
    { }

    public dieEvent onDieEvent;

    public setHpEvent onHpChange=new setHpEvent();

    public AudioCue dieAudio;//在audiomanager中有绑定怪物hpdamable默认《受击》音效的效果


    public override void takeDamage(DamagerBase damager)
    {
        if ( currentHp <= 0)
        {
           // return;
        }

        base.takeDamage(damager);
        addHp(-damager.getDamage(this),damager);

    }


    public void setHp(int hp,DamagerBase damager=null)
    {
        currentHp = hp;
        checkHp(damager);
    }

    protected virtual void checkHp(DamagerBase damager)
    {
        currentHp = Mathf.Clamp(currentHp,0,maxHp);
        onHpChange.Invoke(this);
        if (currentHp == 0)
        {
            die(damager);
        }
    }
    protected void addHp(int number,DamagerBase damager)//如受到伤害 number<0
    {
        setHp(currentHp + number,damager);
    }

    protected virtual void die(DamagerBase damager)
    {
        onDieEvent.Invoke(damager,this);
        Destroy(gameObject);//未完善
        Debug.Log(gameObject.name+" die");
        if (dieAudio)
        {
            dieAudio.PlayAudioCue();
        }

    }



}
