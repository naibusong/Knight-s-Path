using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//每个敌人都共有的组件，提高效率
[RequireComponent(typeof(Rigidbody2D),typeof(Animator),typeof(PhysicsCheck))]
public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public PhysicsCheck physicscheck;
    [Header("基本参数")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public Transform attacker;
    public float hurtForce;
    public Vector3 spwanPoint;//出生点坐标
    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;
    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    [Header("检测")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDis;
    public LayerMask attackLayer;

    private BaseState currentState;
    protected BaseState potrolState;
    protected BaseState chaseState;
    protected BaseState skillState;


    #region 周期函数
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicscheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        spwanPoint = transform.position;//出生点等于初始坐标
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
    #endregion
    public virtual void Move()//virtual虚拟的，子类可以访问修改
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("preMove") && !anim.GetCurrentAnimatorStateInfo(0).IsName("recover"))
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
        if (!FoundPlayer() && lostTimeCounter >=0)
        {
            lostTimeCounter -= Time.deltaTime;
        }
        
    }
    public virtual bool FoundPlayer()//发现玩家
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDis, attackLayer);
    }

    public void SwitchState(NPCState state)//状态切换，枚举
    {
        var newState = state switch
        {
            NPCState.Potrol => potrolState,
            NPCState.Chase => chaseState,
            NPCState.Skill => skillState,
            _ => null
        };

        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    public virtual Vector3 GetNewPoint()
    {
        return transform.position;
    }
    #region 事件函数
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
        rb.velocity =new Vector2(0, rb.velocity.y);
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
        rb.velocity = Vector2.zero;
        gameObject.layer = 2;
        isDead = true;
        anim.SetBool("dead", true);
    }

    public void DestoryAfterAnimation()
    {
        Destroy(this);
    }
    #endregion

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset+ new Vector3(checkDis*-transform.localScale.x,0), 0.2f);
    }
}
