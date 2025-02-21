using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPotrolUpdate : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy; 
    }
    public override void LogicUpdate()
    {
        //TODO 发现玩家后追击玩家
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
        
    }
}
