using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public Vector2 bottomOffset;
    public bool isGround;
    public float checkRaduis;
    public LayerMask groundLayer;
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        //µÿ√ÊºÏ≤‚
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis,groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}
