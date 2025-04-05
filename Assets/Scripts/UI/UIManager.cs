using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerStateBar playerStateBar;
    [Header("�¼�����")]
    public CharacterEventSO healthEvent;
    public SceneLoadEventSO unloadedSceneEvent;
    public ViewEventSO gameOverEvent;
    public ViewEventSO backToMenuEvent;
    public FloatEventSO syncVolumeEvent;

    [Header("�㲥")]
    public ViewEventSO pauseEvent;

    [Header("���")]
    public GameObject gameOverPanel;
    public GameObject backToMenuBtn;
    public GameObject pausePanel;
    public Button settingBtn;
    public Slider volumeSlider;

    private void Awake()
    {
        settingBtn.onClick.AddListener(TogglePausePanel);
    }
    private void TogglePausePanel()
    {
        if (pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            pauseEvent.RaiseEvent();
            pausePanel.SetActive(true);
            Time.timeScale = 0f;

        }
    }

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;//ע���¼�
        unloadedSceneEvent.LoadRequestEvent += OnUnLoadedSceneEvent;
        gameOverEvent.OnEventRaised += OnGameOverEvent;
        syncVolumeEvent.OnEventRaised += OnSyncVolumeEvent;
    }


    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;//ע���¼�
        unloadedSceneEvent.LoadRequestEvent -= OnUnLoadedSceneEvent;
        gameOverEvent.OnEventRaised -= OnGameOverEvent;
        syncVolumeEvent.OnEventRaised -= OnSyncVolumeEvent;
    }

    private void OnSyncVolumeEvent(float amount)
    {
        volumeSlider.value = (amount + 80) / 100;
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
