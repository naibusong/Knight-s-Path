using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    private PlayerInputControl playerInput;//输入
    public Transform playerTrans;
    public GameObject signSprite;
    private bool isShow;
    private IInteractable targetItem;//直接用接口创建物体，无论是传送门还是宝箱都通过这个得到


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
        signSprite.SetActive(isShow);//状态由isShow控制
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
