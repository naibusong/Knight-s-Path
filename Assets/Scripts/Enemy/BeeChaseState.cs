using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeChaseState : BaseState
{
    private Attack attack;
    private Vector3 target;
    private Vector3 moveDir;
    private bool isAttack;
    private float attackRateCounter = 0;//¹¥»÷ÆµÂÊ¼ÆÊ±Æ÷
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        attack = enemy.GetComponent<Attack>();
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
        currentEnemy.anim.SetBool("chase", true);
    }
    public override void LogicUpdate()
    {
        if (currentEnemy.lostTimeCounter <= 0)
        {
            currentEnemy.SwitchState(NPCState.Potrol);
        }
        target = new Vector3(currentEnemy.attacker.position.x-0.1f, currentEnemy.attacker.position.y + 0.4f);
        //ÅÐ¶Ï¹¥»÷¾àÀë
        if(Mathf.Abs(target.x -currentEnemy.transform.position.x) < attack.attackRange && Mathf.Abs(target.y - currentEnemy.transform.position.y) < attack.attackRange)
        {
            //¹¥»÷
            isAttack = true;
            if(!currentEnemy.isHurt)
                currentEnemy.rb.velocity = Vector2.zero;

            //¼ÆÊ±Æ÷
            attackRateCounter -= Time.deltaTime;
            if(attackRateCounter <= 0)
            {
                currentEnemy.anim.SetTrigger("attack");
                attackRateCounter = attack.attackRate;
            }
        }
        else//³¬³ö¹¥»÷·¶Î§
        {
            isAttack=false;
        }
    
    
    }



    public override void PhysicsUpdate()
    {
        if (!currentEnemy.isDead && !currentEnemy.wait && !isAttack)
        {
            currentEnemy.rb.velocity = moveDir * currentEnemy.currentSpeed * Time.deltaTime;
        }
        moveDir = (target - currentEnemy.transform.position).normalized;//·­×ª

        if (moveDir.x > 0)
            currentEnemy.transform.localScale = new Vector3(1, 1, 1);
        if (moveDir.x < 0)
            currentEnemy.transform.localScale = new Vector3(-1, 1, 1);
    }
    public override void OnExit()
    {
        currentEnemy.anim.SetBool("chase", false);
    }
}
