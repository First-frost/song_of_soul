using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
<<<<<<< HEAD
/// 
///ֻ������һ��������˺� �������� �����������
/// </summary>���ߣ����
=======
/// ���ߣ����
///ֻ������һ��������˺� �������� �����������
/// </summary>
>>>>>>> 30f6fd9d (damage test)
public class OneDirectDamable : HpDamable
{
    public bool leftInvulnerable;//��ߵ��˺���Ч ��Ϊfalse���ұߵ��˺���Ч
    public override void takeDamage(DamagerBase damager)
    {
<<<<<<< HEAD

       // hittedEffect();
=======
        if (currentHp <= 0)
        {
            return;
        }

        hittedEffect();
>>>>>>> 30f6fd9d (damage test)
        damageDirection = damager.transform.position - transform.position;
        if ((leftInvulnerable && damageDirection.x < 0) || (!leftInvulnerable && damageDirection.x>0))
        {
            return;
        }
<<<<<<< HEAD
        base.takeDamage(damager);
        
=======
        
        addHp(-damager.getDamage(this));
        if (canHitBack)
            hitBack();
>>>>>>> 30f6fd9d (damage test)
    }
}
