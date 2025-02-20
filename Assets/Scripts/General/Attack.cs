using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;//ÉËº¦ÊýÖµ
    public float attackRange;//·¶Î§
    public float attackRate;//ÆµÂÊ

    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Character>()?.TakeDamege(this);
    }
}
