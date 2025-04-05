using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public PlayerStateBar playerStateBar;
    [Header("�¼�����")]
    public CharacterEventSO healthEvent;
    public SceneLoadEventSO unloadedSceneEvent;
    public ViewEventSO gameOverEvent;
    public ViewEventSO backToMenuEvent;

    [Header("���")]
    public GameObject gameOverPanel;
    public GameObject backToMenuBtn;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;//ע���¼�
        unloadedSceneEvent.LoadRequestEvent += OnUnLoadedSceneEvent;
        gameOverEvent.OnEventRaised += OnGameOverEvent;
    }


    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;//ע���¼�
        unloadedSceneEvent.LoadRequestEvent -= OnUnLoadedSceneEvent;
        gameOverEvent.OnEventRaised -= OnGameOverEvent;
    }

    private void OnGameOverEvent()
    {
        gameOverPanel.SetActive(true);
    }

    private void OnUnLoadedSceneEvent(GameSceneSO sceneToLoad, Vector3 arg1, bool arg2)
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
        var percentage = character.currentHealth / character.maxHealth;//���ݵ�PlayerStateBar
        playerStateBar.OnHealthChange(percentage);
    }
}
