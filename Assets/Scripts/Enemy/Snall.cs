using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snall : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        potrolState = new SnallPotrolUpdate();
        skillState = new SnallSkillState();
    }
}
