using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("��������")]
    public float maxHealth;
    public float currentHealth;

    [Header("�޵�֡")]
    public float untouchableTime;
    public float untouchableCounter;//��ʱ��
    public bool untouchable;//�޵�״̬

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
            //ִ������
            OnTakeDamage?.Invoke(attaker.transform);
            TriggerUntouchable();
        }
        else
        {
            currentHealth = 0;//��ɫ����
            OnDead?.Invoke();
        }
        OnHealthChange?.Invoke(this);
    }
    private void TriggerUntouchable()//���������޵�
    {
        if (!untouchable)
        {
            untouchable = true;
            untouchableCounter = untouchableTime;
        }
    }
}
