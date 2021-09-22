using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ײ���ĳ����� ��һ����ʽ��Ҫ
/// </summary>���ߣ����
public abstract class ColliderBase : MonoBehaviour
{
    public LayerMask targetLayer;//������collider��layer
    public bool canWork;

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (canWork && targetLayer.Contains(collision.gameObject) )
        {
            enterEvent();
            canWork = false;
        }
    }

    protected abstract void enterEvent();

}

