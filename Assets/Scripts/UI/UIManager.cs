using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStateBar playerStateBar;
    [Header("�¼�����")]
    public CharacterEventSO healthEvent;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;//ע���¼�
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;//ע���¼�

    }

    private void OnHealthEvent(Character character)
    {
        var percentage = character.currentHealth / character.maxHealth;//���ݵ�PlayerStateBar
        playerStateBar.OnHealthChange(percentage);
    }
}
