using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;//伤害数值
    public float attackRange;//范围
    public float attackRate;//频率

    private void OnTriggerStay2D(Collider2D other)
    {//敌人和主角都使用了takedamege方法，攻击判定范围和主角有重合则自己会扣血，因此要改LayerMask
        other.GetComponent<Character>()?.TakeDamege(this);
    }
}
