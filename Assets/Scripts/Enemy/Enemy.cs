using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    public Animator anim;
    public PhysicsCheck physicscheck;
    [Header("��������")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public Transform attacker;
    public float hurtForce;
    [Header("��ʱ��")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    [Header("״̬")]
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

    private void OnEnable()//����ʱ
    {
        currentState = potrolState;//����Ѳ��״̬
        currentState.OnEnter(this);
    }

    private void OnDisable()//�ر�ʱ
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
    public virtual void Move()//virtual����ģ�������Է����޸�
    {
        rb.velocity = new Vector2(faceDir.x * currentSpeed * Time.deltaTime, rb.velocity.y);
    }

    //��ʱ��
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
        //ת��
        if (attackerTrans.position.x - transform.position.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        if(attackerTrans.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(1, 1, 1);
        //����Ч��
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
