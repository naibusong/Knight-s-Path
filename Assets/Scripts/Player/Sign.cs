using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    private PlayerInputControl playerInput;//����
    public Transform playerTrans;
    public GameObject signSprite;
    private bool isShow;
    private IInteractable targetItem;//ֱ���ýӿڴ������壬�����Ǵ����Ż��Ǳ��䶼ͨ������õ�


    private void Awake()
    {
        playerInput = new PlayerInputControl();
        playerInput.Enable();
    }
    private void OnEnable()
    {
        playerInput.GamePlay.Confirm.started += OnConfirm;
    }
    private void OnDisable()
    {
        isShow = false;
    }

    private void Update()
    {
        signSprite.SetActive(isShow);//״̬��isShow����
        signSprite.transform.localScale = playerTrans.localScale;
        //playerInput = new PlayerInputControl();
        //playerInput.Enable();
    }

    private void OnConfirm(InputAction.CallbackContext obj)
    {
        if (isShow)
        {
            targetItem.TriggerAction();
            GetComponent<AudioDefination>().PlayAudioClip();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactive"))
        {
            isShow = true;
            targetItem = other.GetComponent<IInteractable>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isShow = false;
    }

}
