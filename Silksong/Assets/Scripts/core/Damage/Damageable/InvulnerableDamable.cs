using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.Events;
/// 
/// ��hpdamable�Ļ����� �����ܻ����޵л���
/// </summary>���ߣ����
=======
/// <summary>
/// ���ߣ����
/// ��hpdamable�Ļ����� �����ܻ����޵л���
/// </summary>
>>>>>>> 30f6fd9d (damage test)
public class InvulnerableDamable : HpDamable
{
    public bool invulnerableAfterDamage = true;//���˺��޵�
    public float invulnerabilityDuration = 3f;//�޵�ʱ��
    protected float inulnerabilityTimer;
<<<<<<< HEAD

=======
>>>>>>> 30f6fd9d (damage test)
    public override void takeDamage(DamagerBase damager)
    {
        base.takeDamage(damager);
        if(invulnerableAfterDamage)
        {
            enableInvulnerability();
        }
    }



    public void enableInvulnerability(bool ignoreTimer = false)//�����޵�
    {
        invulnerable = true;
        //technically don't ignore timer, just set it to an insanly big number. Allow to avoid to add more test & special case.
        inulnerabilityTimer = ignoreTimer ? float.MaxValue : invulnerabilityDuration;
    }

    void Update()
    {
<<<<<<< HEAD
        if (invulnerable&&invulnerableAfterDamage)
=======
        if (invulnerable)
>>>>>>> 30f6fd9d (damage test)
        {
            inulnerabilityTimer -= Time.deltaTime;

            if (inulnerabilityTimer <= 0f)
            {
                invulnerable = false;
<<<<<<< HEAD
                GetComponent<Collider2D>().enabled = false;//���¼���һ��collider �������޵�ʱ����trigger�� �����޵й���ontriggerEnter������
                GetComponent<Collider2D>().enabled = true;
=======
                GetComponent<BoxCollider2D>().enabled = false;//���¼���һ��collider �������޵�ʱ����trigger�� �����޵й���ontriggerEnter������
                GetComponent<BoxCollider2D>().enabled = true;
>>>>>>> 30f6fd9d (damage test)
            }
        }
    }


}
