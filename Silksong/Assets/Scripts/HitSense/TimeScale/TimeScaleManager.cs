using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    public static TimeScaleManager Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<TimeScaleManager>();

            if (instance != null)
                return instance;

            GameObject sceneControllerGameObject = new GameObject("TimeScaleManage");
            instance = sceneControllerGameObject.AddComponent<TimeScaleManager>();

            return instance;
        }
    }//����

    protected static TimeScaleManager instance;
    void Start()
    {
        
    }

    public void changeTimeScaleForFrames(int framesCount,float scale)
    {
        float time = framesCount / 60f;//Ĭ��60fps ��Ҫ�޸�
        changeTimeScaleForSeconds(time,scale);
    }

    public void changeTimeScaleForSeconds(float time,float scale)
    {
        StartCoroutine(IChangeTimeScaleForSeconds(time, scale));
    }
    private IEnumerator IChangeTimeScaleForSeconds(float time,float scale)
    {
        //print(time);
        Time.timeScale = scale;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
    }
}
