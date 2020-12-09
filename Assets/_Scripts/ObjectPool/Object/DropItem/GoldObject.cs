using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldObject : DropItemObject
{
    public int goldNum = 0;

    public void SetGoldNum(int level)
    {
        goldNum = (int)(Random.Range(5, 10f) * level);
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
        goldNum = 0;
    }
}
