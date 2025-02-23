using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public PlayerController playerController;
    private Rigidbody2D rb;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public float checkRaduis;
    public LayerMask groundLayer;
    [Header("״̬")]
    public bool isGround;
    public bool touLeftWall;
    public bool touRightWall;
    public bool OnWall;
    public bool isPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if(isPlayer)
            playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        //������
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), checkRaduis,groundLayer);
        //ǽ�ڼ��
        touLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        touRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);
        
        //��ǽ����
        if(isPlayer)
            OnWall = (touLeftWall && playerController.inputDirection.x < 0f || touRightWall && playerController.inputDirection.x >0f) && rb.velocity.y <0;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}
