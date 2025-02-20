using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    [Header("基本参数")]
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
    public virtual void Move()//虚拟的，子类可以访问修改
    {
        rb.velocity = new Vector2(faceDir.x * currentSpeed *Time.deltaTime, rb.velocity.y);   
    }
}
