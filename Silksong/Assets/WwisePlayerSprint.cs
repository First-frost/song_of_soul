using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwisePlayerSprint : MonoBehaviour
{
    public AK.Wwise.Event MyEvent;
    // ʹ�ô˺������г�ʼ����
    public void Wwiseplayersprint()
    {
        MyEvent.Post(gameObject);
    }
}