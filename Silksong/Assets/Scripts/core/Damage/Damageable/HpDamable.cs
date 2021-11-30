using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
/// <summary>
/// 
/// ӵ������ֵ����ط�����damable 
/// </summary>���ߣ����
public class HpDamable :Damable
{
    public int maxHp ;//�������ֵ

    public int currentHp;//��ǰhp

    public bool resetHealthOnSceneReload;

    [Serializable]
    public class dieEvent : UnityEvent<DamagerBase, DamageableBase>
    { }

    public dieEvent onDieEvent;


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
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        if (currentHp <= 0)
        {
            die(damager);
        }
    }
    protected void addHp(int number,DamagerBase damager)//���ܵ��˺� number<0
    {
        currentHp += number;
        checkHp(damager);

    }

    protected virtual void die(DamagerBase damager)
    {
        onDieEvent.Invoke(damager,this);
       // Destroy(gameObject);//δ����
        Debug.Log(gameObject.name+" die");
    }



}
