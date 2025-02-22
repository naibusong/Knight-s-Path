using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    [Header("ÒÆ¶¯·¶Î§")]
    public float potrolRaduis;

    protected override void Awake()
    {
        base.Awake();
        potrolState = new BeePotrolUpdate();
        chaseState = new BeeChaseState();
    }
    public override bool FoundPlayer()
    {
        var obj =  Physics2D.OverlapCircle(transform.position, checkDis, attackLayer);
        if (obj)
        {
            attacker = obj.transform;
        }
        return obj;
    }
    public override void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkDis);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, potrolRaduis);
    }

    public override Vector3 GetNewPoint()
    {
        var targetX = Random.Range(-potrolRaduis,potrolRaduis);
        var targetY = Random.Range(-potrolRaduis,potrolRaduis);

        return spwanPoint + new Vector3(targetX, targetY);
    }

    public override void Move()
    {
        
    }
}
