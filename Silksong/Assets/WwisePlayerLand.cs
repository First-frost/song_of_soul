using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwisePlayerLand : MonoBehaviour
{
    public AK.Wwise.Event MyEvent;
    // ʹ�ô˺������г�ʼ����
    public void Wwiseplayerland()
    {
        MyEvent.Post(gameObject);
    }
}