using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    //private PlayerInputControl playerInput;// ‰»Î
    public Transform playerTrans;
    public GameObject signSprite;
    private bool isShow;

    private void Update()
    {
        signSprite.SetActive(isShow);//◊¥Ã¨”…isShowøÿ÷∆
        signSprite.transform.localScale = playerTrans.localScale;
        //playerInput = new PlayerInputControl();
        //playerInput.Enable();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactive"))
        {
            isShow = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isShow = false;
    }

}
