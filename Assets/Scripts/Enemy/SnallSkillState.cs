using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnallSkillState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0;
        currentEnemy.anim.SetBool("walk", false);
        currentEnemy.anim.SetBool("hide", true);
        currentEnemy.anim.SetTrigger("skill");

        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
        currentEnemy.GetComponent<Character>().untouchable = true;
        currentEnemy.GetComponent<Character>().untouchableCounter = currentEnemy.lostTimeCounter;
    }
    public override void LogicUpdate()
    {
        if(currentEnemy.lostTimeCounter <= 0)
        {
            currentEnemy.SwitchState(NPCState.Potrol);
        }
        currentEnemy.GetComponent<Character>().untouchableCounter = currentEnemy.lostTimeCounter;
    }

    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy.GetComponent<Character>().untouchable = false;
        currentEnemy.anim.SetBool("hide", false);
    }
}
