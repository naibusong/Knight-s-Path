using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStateBar playerStateBar;
    [Header("事件监听")]
    public CharacterEventSO healthEvent;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;//注册事件
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;//注销事件

    }

    private void OnHealthEvent(Character character)
    {
        var percentage = character.currentHealth / character.maxHealth;//传递到PlayerStateBar
        playerStateBar.OnHealthChange(percentage);
    }
}
