using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.SetBool("run", true);
        Debug.Log("enter");
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
    }
    public override void LogicUpdate()
    {
        if(currentEnemy.lostTimeCounter <= 0)
        {
            currentEnemy.SwitchState(NPCState.Potrol);
        }
        if (!currentEnemy.physicscheck.isGround || (currentEnemy.physicscheck.touLeftWall && currentEnemy.faceDir.x > 0) || (currentEnemy.physicscheck.touRightWall && currentEnemy.faceDir.x < 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
        }
    }

    public override void PhysicsUpdate()
    {

    }
    public override void OnExit()
    {
        currentEnemy.lostTimeCounter =currentEnemy.lostTime;
        currentEnemy.anim.SetBool("run", false);
    }
    


}
