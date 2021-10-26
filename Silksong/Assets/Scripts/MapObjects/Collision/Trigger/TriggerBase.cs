using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ײ���ĳ����� ��һ����ʽ��Ҫ
/// </summary>���ߣ����
public abstract class TriggerBase : MonoBehaviour
{
    public LayerMask targetLayer;//������trigger��layer
    public bool canWork;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (canWork && targetLayer.Contains(collision.gameObject) )
        {
            enterEvent();
            canWork = false;
        }
    }

    protected abstract void enterEvent();

}
