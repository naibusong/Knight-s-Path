using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基础属性")]
    public float maxHealth;
    public float currentHealth;

    [Header("无敌帧")]
    public float untouchableTime;
    public float untouchableCounter;//计时器
    public bool untouchable;//无敌状态

    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDead;
    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }

    private void Update()
    {
        if (untouchable)
        {
            untouchableCounter -= Time.deltaTime;
            if(untouchableCounter <= 0)
            {
                untouchable = false;
            }
        }
    }
    public void TakeDamege(Attack attaker)
    {
        //Debug.Log(attaker.damage);
        if (untouchable)
            return;
        if (currentHealth - attaker.damage > 0)
        {
            currentHealth -= attaker.damage;
            //执行受伤
            OnTakeDamage?.Invoke(attaker.transform);
            TriggerUntouchable();
        }
        else
        {
            currentHealth = 0;//角色死亡
            OnDead?.Invoke();
        }
        OnHealthChange?.Invoke(this);
    }
    private void TriggerUntouchable()//触发受伤无敌
    {
        if (!untouchable)
        {
            untouchable = true;
            untouchableCounter = untouchableTime;
        }
    }
}
