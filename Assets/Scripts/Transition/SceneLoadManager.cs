using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoadManager : MonoBehaviour
{
    public Vector3 firstPosition;
    public Transform playerTrans;
    private SpriteRenderer playerRenderer;
    [Header("�¼�����")]
    public SceneLoadEventSO loadEventSO;
    public ViewEventSO newGameEvent;
    public ViewEventSO backToMenuEvent;

    [Header("�㲥")]
    public ViewEventSO afterSceneLoadEvent;
    public FadeEventSO fadeEvent;
    public SceneLoadEventSO unLoadedSceneEvent;

    [Header("����")]
    public GameSceneSO firstLoadScene;
    public GameSceneSO menuScene;
    private GameSceneSO currentLoadScene;
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScene;
    public float fadeTime;
    private bool isLoading;

    [Header("���")]
    public GameObject gameOverPanel;
    public GameObject victoryPanel;

    private void Awake()
    {
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        //currentLoadScene = firstLoadScene;
        //currentLoadScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
        playerRenderer = playerTrans.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        OnLoadRequestEvent(menuScene, firstPosition, true);
        playerRenderer.enabled = false;
        //NewGamee();
    }
    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
        newGameEvent.OnEventRaised += NewGamee;
        backToMenuEvent.OnEventRaised += OnBackToMenuEvent;


    }
    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
        newGameEvent.OnEventRaised -= NewGamee;
        backToMenuEvent.OnEventRaised += OnBackToMenuEvent;
    }

    private void OnBackToMenuEvent()
    {
        sceneToLoad = menuScene;
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
        loadEventSO.RaiseLoadRequestEvent(sceneToLoad, firstPosition, true);
    }

    /// <summary>
    /// ���������¼�����
    /// </summary>
    /// <param name="locationToLoad"></param>
    /// <param name="posToGo"></param>
    /// <param name="fadeScene"></param>
    /// 
    private void NewGamee()
    {
        sceneToLoad = firstLoadScene;
        OnLoadRequestEvent(sceneToLoad,firstPosition,true);
        playerRenderer.enabled = true;
    }

    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScene)
    {
        if (isLoading)
            return;
        isLoading = true;
        sceneToLoad = locationToLoad;
        positionToGo = posToGo;
        this.fadeScene = fadeScene;
        //Debug.Log("123");
        if (currentLoadScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }
        else
        {
            LoadNewScene();
        }
    }

    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScene)
        {
            //ʵ�ֽ��뽥��
            fadeEvent.FadeIn(fadeTime);
        }
        yield return new WaitForSeconds(fadeTime);//�ȴ�������ȫ���

        //�㲥�¼�����Ѫ����ʾ
        unLoadedSceneEvent.RaiseLoadRequestEvent(sceneToLoad, positionToGo, true);

        currentLoadScene.sceneReference.UnLoadScene();//ж��
        //�ر�����
        playerTrans.gameObject.SetActive(false);
        LoadNewScene();
    }
    private void LoadNewScene()
    {
        var loadingOption =  sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
    }
    /// <summary>
    /// ����������ɺ�
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadScene = sceneToLoad;
        playerTrans.position = positionToGo;
        //��������
        playerTrans.gameObject.SetActive(true);
        if (fadeScene)
        {
            //TODO:
            fadeEvent.FadeOut(fadeTime);
        }
        isLoading = false;
        afterSceneLoadEvent.RaiseEvent();
        //2025.4.6 add
        //newGameEvent.RaiseEvent();
    }
}
