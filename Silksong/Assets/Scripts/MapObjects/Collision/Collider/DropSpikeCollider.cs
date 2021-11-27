using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///��������������� ֹͣ��ȡ���˺�
/// </summary>���ߣ����
public class DropSpikeCollider :Collider2DBase
{
    public GameObject damager;
    public float gScale;
    protected override void enterEvent()//����ground layer
    {
        damager.SetActive(false);
    }

    public void drop()//��ʼ����
    {
        GetComponent<Rigidbody2D>().gravityScale = gScale;
        damager.SetActive(true);
    }
}
