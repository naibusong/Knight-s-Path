using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour,IInteractable
{
    private SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    public Sprite closeSprite;
    public bool isDone;

    [Header("广播")]
    public ViewEventSO victoryEvent;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        spriteRenderer.sprite = isDone? openSprite : closeSprite;
    }
    public void TriggerAction()
    {
        Debug.Log("Open");
        if (!isDone)//没有被打开过
        {
            OpenChest();
        }
        victoryEvent.RaiseEvent();
    }

    private void OpenChest()
    {
        spriteRenderer.sprite = openSprite;
        this.gameObject.tag = "Untagged";
    }
}
