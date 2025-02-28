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
    private PlayerAnimation playerAnimation;
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    public float hurtForcr;
    public float wallSlideForce;
    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    public bool wallJump;
    private void Awake()
    {
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        physicscheck = GetComponent<PhysicsCheck>();
        inputControl.GamePlay.Jump.started += Jump;
        inputControl.GamePlay.Attack.started += PlayerAttack;
        playerAnimation = GetComponent<PlayerAnimation>();
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
        if(!isHurt)
            Move();
        CheckState();
    }

    public void Move()
    {
        if (!wallJump)
        {
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        }
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
        if (physicscheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);//瞬时的力
            GetComponent<AudioDefination>().PlayAudioClip();
        }
        if (physicscheck.OnWall)
        {
            rb.AddForce(new Vector2(-inputDirection.x, 2.5f) * wallSlideForce, ForceMode2D.Impulse);
            wallJump = true;
            GetComponent<AudioDefination>().PlayAudioClip();
        }
    }

    //攻击
    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayAttack();
        isAttack = true;
    }


    #region UnityEvent
    public void OnHurt(Transform attaker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attaker.position.x),0).normalized;//归一化，只在0-1之间
        rb.AddForce(dir * hurtForcr, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.GamePlay.Disable();

    }
    #endregion
    private void CheckState()
    {
        if (physicscheck.OnWall)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        else
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        if(wallJump && rb.velocity.y < 0)
        {
            wallJump = false;
        }
    }
}

