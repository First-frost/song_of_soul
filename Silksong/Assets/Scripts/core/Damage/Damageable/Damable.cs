

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

}
