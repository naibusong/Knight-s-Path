using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStateBar playerStateBar;
    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    public SceneLoadEventSO loadEvent;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;//注册事件
        loadEvent.LoadRequestEvent += OnLoadEvent;
    }


    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;//注销事件
        loadEvent.LoadRequestEvent -= OnLoadEvent;
    }
    private void OnLoadEvent(GameSceneSO sceneToLoad, Vector3 arg1, bool arg2)
    {
        if(sceneToLoad.sceneType == SceneType.Menu)
        {
            playerStateBar.gameObject.SetActive(false);
        }
        if (sceneToLoad.sceneType == SceneType.Location)
        {
            playerStateBar.gameObject.SetActive(true);
        }
    }

    private void OnHealthEvent(Character character)
    {
        var percentage = character.currentHealth / character.maxHealth;//传递到PlayerStateBar
        playerStateBar.OnHealthChange(percentage);
    }
}
