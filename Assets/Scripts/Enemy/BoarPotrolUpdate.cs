using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPotrolUpdate : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }
    public override void LogicUpdate()
    {
        // 发现玩家后追击玩家
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }

        if (!currentEnemy.physicscheck.isGround || (currentEnemy.physicscheck.touLeftWall && currentEnemy.faceDir.x > 0) || (currentEnemy.physicscheck.touRightWall && currentEnemy.faceDir.x < 0))
        {
            currentEnemy.wait = true;
            currentEnemy.anim.SetBool("walk", false);//碰墙激活idle动画
        }
        else
        {
            currentEnemy.anim.SetBool("walk", true);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk", false);
        Debug.Log("Exit");
    }
}
