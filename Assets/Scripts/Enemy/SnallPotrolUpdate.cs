using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnallPotrolUpdate : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }
    public override void LogicUpdate()
    {
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Skill);
        }
        if (!currentEnemy.physicscheck.isGround || (currentEnemy.physicscheck.touLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicscheck.touRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.anim.SetBool("walk", false);//ÅöÇ½¼¤»îidle¶¯»­
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
