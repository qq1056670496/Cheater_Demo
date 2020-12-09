using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonState : EnemyState
{

    public override void OnDead()
    {
        base.OnDead();
        QuestManager.Instance.KillSkeletonNum++;
    }
}
