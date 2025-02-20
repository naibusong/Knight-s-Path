using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    [Header("��������")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x,0,0);
    }

    private void FixedUpdate()
    {
        Move();
    }
    public virtual void Move()//����ģ�������Է����޸�
    {
        rb.velocity = new Vector2(faceDir.x * currentSpeed *Time.deltaTime, rb.velocity.y);   
    }
}
