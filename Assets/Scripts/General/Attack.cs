using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;//�˺���ֵ
    public float attackRange;//��Χ
    public float attackRate;//Ƶ��

    private void OnTriggerStay2D(Collider2D other)
    {//���˺����Ƕ�ʹ����takedamege�����������ж���Χ���������غ����Լ����Ѫ�����Ҫ��LayerMask
        other.GetComponent<Character>()?.TakeDamege(this);
    }
}
