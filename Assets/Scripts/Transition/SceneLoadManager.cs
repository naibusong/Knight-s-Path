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
    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;
    public ViewEventSO newGameEvent;
    public ViewEventSO backToMenuEvent;

    [Header("广播")]
    public ViewEventSO afterSceneLoadEvent;
    public FadeEventSO fadeEvent;
    public SceneLoadEventSO unLoadedSceneEvent;

    [Header("场景")]
    public GameSceneSO firstLoadScene;
    public GameSceneSO menuScene;
    private GameSceneSO currentLoadScene;
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScene;
    public float fadeTime;
    private bool isLoading;

    [Header("组件")]
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
    /// 场景加载事件请求
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
            //实现渐入渐出
            fadeEvent.FadeIn(fadeTime);
        }
        yield return new WaitForSeconds(fadeTime);//等待场景完全变黑

        //广播事件调整血条显示
        unLoadedSceneEvent.RaiseLoadRequestEvent(sceneToLoad, positionToGo, true);

        currentLoadScene.sceneReference.UnLoadScene();//卸载
        //关闭人物
        playerTrans.gameObject.SetActive(false);
        LoadNewScene();
    }
    private void LoadNewScene()
    {
        var loadingOption =  sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
    }
    /// <summary>
    /// 场景加载完成后
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadScene = sceneToLoad;
        playerTrans.position = positionToGo;
        //启动人物
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
