using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using System;
using UnityEngine.Events;

=======
/// <summary>
/// ���ߣ����
/// damager�ĳ������ 
/// damager����triggerӦ��Ϊ���������� ���Լ���layer ������������
/// </summary>
>>>>>>> 30f6fd9d (damage test)
public static class LayerMaskExtensions//layerMask����contains���� �ж�gameObject�Ƿ���layerMask��
{
    public static bool Contains(this LayerMask layers, GameObject gameObject)
    {
        return 0 != (layers.value & 1 << gameObject.layer);
    }
}

<<<<<<< HEAD
/// <summary>
/// 
/// damager�ĳ������ 
/// </summary>���ߣ����
=======
>>>>>>> 30f6fd9d (damage test)

public abstract class DamagerBase : MonoBehaviour
{
    public bool ignoreInvincibility = false;//�����޵�
    public bool canDamage = true;
    public int damage;//�˺���ֵ
<<<<<<< HEAD
    public Vector2 beatBackVector = Vector2.zero;

    [Serializable]
    public class DamableEvent : UnityEvent<DamagerBase, DamageableBase>
    { }

    public DamableEvent makeDamageEvent;
=======
>>>>>>> 30f6fd9d (damage test)


    public virtual int getDamage(DamageableBase target)//�����ɵľ����˺���ֵ
    {
        return damage;
    }
 
    protected abstract void makeDamage(DamageableBase target);//����˺����Ч��

    protected virtual void OnTriggerEnter2D(Collider2D collision)//����gameobjrect��layer��project setting ȷ����Щlayer������ײ
    {
        if(!canDamage)
        {
            return;
        }
        DamageableBase damageable = collision.GetComponent<DamageableBase>();//ֻ��ӵ��Damageable�����collider�ܹ���
        if (damageable )
        {
            //Debug.Log(damageable.gameObject.name + " ontrigger");
            if (!ignoreInvincibility && damageable.invulnerable)
            {
                return;
            }
            damageable.takeDamage(this);
            makeDamage(damageable);

        }
    }
}
