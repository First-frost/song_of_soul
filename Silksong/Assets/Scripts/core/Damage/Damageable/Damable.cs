<<<<<<< HEAD

using UnityEngine;
public class Damable : DamageableBase
{
    protected override void Awake()
    {
        base.Awake();     
    }
    public override void takeDamage(DamagerBase damager)
    {
        damageDirection = damager.transform.position - transform.position;
        takeDamageEvent.Invoke(damager,this);

        if(takeDamageAudio)
        {
            takeDamageAudio.PlayAudioCue();
        }

        if(takeDamageSfxSO)
        {
            //Debug.Log("creat hitted sfx");
            Vector2 hittedPosition = Vector2.zero;

            hittedPosition=GetComponent<Collider2D>().bounds.ClosestPoint(damager.transform.position);

            SfxManager.Instance.creatHittedSfx(hittedPosition, hittedPosition-(Vector2)transform.position ,takeDamageSfxSO);
        }

    }

=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ߣ����
/// ��򵥵�damable
/// </summary>
/// 
public class Damable : DamageableBase
{
    public override void takeDamage(DamagerBase damager)
    {
        hittedEffect();//������event.invoke()
    }

    protected virtual void hittedEffect()//�ܻ�Ч�� ���б�Ҫ���¼���ʽ����
    {
        //Debug.Log(gameObject.name + " is hitted");
    }
>>>>>>> 30f6fd9d (damage test)

}
