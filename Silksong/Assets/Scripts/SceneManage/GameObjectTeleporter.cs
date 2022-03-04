using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
/// <summary>
/// ������Ϸ�����ڳ����ڵĴ��� ����
/// </summary>
public class GameObjectTeleporter : MonoBehaviour
{
    public static GameObjectTeleporter Instance
    {
        get
        {
           // Debug.Log("get");
            if (instance != null)
                return instance;

            instance = FindObjectOfType<GameObjectTeleporter>();

            if (instance != null)
                return instance;

            GameObject gameObjectTeleporter = new GameObject("GameObjectTeleporter");
            instance = gameObjectTeleporter.AddComponent<GameObjectTeleporter>();

            return instance;
        }
    }

    protected static GameObjectTeleporter instance;

    protected PlayerInput playerInput;//������������ҽ�ɫʱ �����һ������ҽ�ɫ��
    public Vector3 playerRebornPoint;//������µ�������
    public bool Transitioning;

    public CinemachineVirtualCamera virtualCamera;
    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        playerInput = FindObjectOfType<PlayerInput>();
    }

    public static void playerReborn()
    {
        //Instance.playerInput.transform.localScale = new Vector3(1, 0, 0);
        Teleport(Instance.playerInput.gameObject,Instance.playerRebornPoint);
    }
    public  void playerEnterScene(SceneEntrance.EntranceTag entranceTag)//����ҽ����³���ʱ���ø÷���
    {
        SceneEntrance entrance = SceneEntrance.GetDestination(entranceTag);
        if(entrance==null)//�ó���û����� ��������ҵ���Ϸ���� 
        {
            return;
        }
        if (playerInput == null)
            playerInput = FindObjectOfType<PlayerInput>();//

        //playerInput.transform.localScale = new Vector3();��ɫ���� ��δ����
        playerRebornPoint = entrance.transform.position;

        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if(virtualCamera)
        virtualCamera.Follow = playerInput.transform;

        GameManager.Instance.audioManager.setMonstersDefaultHittedAudio();

        Teleport(playerInput.gameObject, entrance.transform.position);
    }
    public static void Teleport(GameObject transitioningGameObject, Vector3 destinationPosition)
    {
        Instance.StartCoroutine(Instance.Transition(transitioningGameObject, false, false, destinationPosition, false));
    }

    protected IEnumerator Transition(GameObject transitioningGameObject, bool releaseControl, bool resetInputValues, Vector3 destinationPosition, bool fade)
    {
        Transitioning = true;

        if(playerInput==null)
        playerInput = FindObjectOfType<PlayerInput>();

        if (releaseControl)
        {
            playerInput.ReleaseControls(resetInputValues);
        }

        /*  if (fade)
              yield return StartCoroutine(ScreenFader.FadeSceneOut());*///�������ɼ����ݲ����� ����yield return null�����ֹû�з���ֵ
        transitioningGameObject.transform.position = destinationPosition;
        yield return null;

   

       /* if (fade)
            yield return StartCoroutine(ScreenFader.FadeSceneIn());*/

        if (releaseControl)
        {
            playerInput.GainControls();
        }

        Transitioning = false;
    }

}
