using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem.iOS;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoadManager : MonoBehaviour
{
    public Vector3 firstPosition;
    public Transform playerTrans;
    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;

    [Header("广播")]
    public ViewEventSO afterSceneLoadEvent;
    private GameSceneSO currentLoadScene;
    private GameSceneSO sceneToLoad;
    public FadeEventSO fadeEvent;
    private Vector3 positionToGo;
    private bool fadeScene;
    public float fadeTime;
    private bool isLoading;

    private void Awake()
    {
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        //currentLoadScene = firstLoadScene;
        //currentLoadScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }
    private void Start()
    {
        NewGamee();
    }
    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
    }
    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
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
        yield return new WaitForSeconds(fadeTime);
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
    }
}
