using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public PlayerInputControl inputControl;
    public Vector2 inputDirection;
    public PhysicsCheck physicscheck;
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    private void Awake()
    {
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        physicscheck = GetComponent<PhysicsCheck>();
        inputControl.GamePlay.Jump.started += Jump;
        
    }


    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();

    }

    //测试
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("1");
    }
    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        int faceDir = (int) transform .localScale.x;

        if(inputDirection.x < 0)
            faceDir = -1;
        if(inputDirection.x > 0)
            faceDir = 1;
        //人物翻转
        transform.localScale = new Vector3(faceDir, 1, 1);
    }
    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("jump");
        if(physicscheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);//瞬时的力
    }
}
