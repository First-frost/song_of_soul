using UnityEngine;
/// <summary>
/// �����̵Ĵ�����
/// </summary>���ߣ����
public class SpikeDropTrigger : TriggerBase
{
    public GameObject spike;
    public float dropSpeed;
    protected override void enterEvent()
    {
        Rigidbody2D rigidbody = spike.GetComponent<Rigidbody2D>();
        if(rigidbody)
        {
            //Debug.Log("drop");
            rigidbody.velocity = new Vector2(0, -dropSpeed);
        }
    }
}
