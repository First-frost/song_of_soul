using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
<<<<<<< HEAD
/// 
/// /���������˺��� ����������������damager ����ˮ  ������δʵ�� 
/// </summary>���ߣ����
public class RebornDamager:TwoTargetDamager
{
    public LayerMask rebornLayer;
    void Start()
    {
    }

    protected  override void makeDamage(DamageableBase damageable)
    {
        base.makeDamage(damageable);
        if(rebornLayer.Contains(damageable.gameObject) && (damageable as HpDamable).CurrentHp>0 )
        {
            GameObjectTeleporter.playerReborn();
        }
=======
/// ���ߣ����
/// /���������˺��� ����������������damager ����ˮ  ������δʵ�� ������kit����
/// </summary>
public class RebornDamager:TwoTargetDamager
{
    //private SceneManger sceneManger;
    public LayerMask rebornLayer;
    void Start()
    {
        //sceneManger = GameObject.Find("Enviroment").GetComponent<SceneManger>();
    }

    protected  override void makeDamage(DamageableBase damageable)//
    {
        // base.makeDamge(Damageable);
      /*  if(rebornLayer.Contains(damageable.gameObject))
        {
            sceneManger.rebornPlayer();
        }*/

>>>>>>> 30f6fd9d (damage test)
    }
}
