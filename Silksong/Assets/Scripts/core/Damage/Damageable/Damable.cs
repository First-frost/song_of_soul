

public class Damable : DamageableBase
{
    public override void takeDamage(DamagerBase damager)
    {
        //  hittedEffect();
        damageDirection = damager.transform.position - transform.position;
        EventsManager.Instance.Invoke(gameObject,EventType.onTakeDamage);
    }

    /*protected virtual void hittedEffect()//�ܻ�Ч�� ���б�Ҫ���¼���ʽ����
    {
        //Debug.Log(gameObject.name + " is hitted");
    }*/

}
