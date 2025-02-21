using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    public Animator anim;
    public PhysicsCheck physicscheck;
    [Header("基本参数")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public Transform attacker;
    public float hurtForce;
    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    [Header("状态")]
    public bool isHurt;
    public bool isDead;

    private BaseState currentState;
    protected BaseState potrolState;
    protected BaseState chaseState;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicscheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;

    }

    private void OnEnable()//激活时
    {
        currentState = potrolState;//进入巡逻状态
        currentState.OnEnter(this);
    }

    private void OnDisable()//关闭时
    {
        currentState.OnExit();
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        
        currentState.LogicUpdate();
        TimeCounter();
    }

    private void FixedUpdate()
    {
        if(!isHurt && !isDead && !wait)
            Move();
        currentState.PhysicsUpdate();
    }
    public virtual void Move()//virtual虚拟的，子类可以访问修改
    {
        rb.velocity = new Vector2(faceDir.x * currentSpeed * Time.deltaTime, rb.velocity.y);
    }

    //计时器
    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter < 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }
    }
    public void OnTakeDamage(Transform attackerTrans)
    {
        attacker = attackerTrans;
        //转身
        if (attackerTrans.position.x - transform.position.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        if(attackerTrans.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(1, 1, 1);
        //击退效果
        isHurt = true;
        anim.SetBool("hurt", true);
        Vector2 dir = new Vector2(transform.position.x - attackerTrans.position.x, 0).normalized;
        StartCoroutine(OnHurt(dir));
    }

    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isHurt=false;
    }

    public void OnDie()
    {
        gameObject.layer = 2;
        isDead = true;
        anim.SetBool("dead", true);
    }

    public void DestoryAfterAnimation()
    {
        Destroy(this);
    }
}
