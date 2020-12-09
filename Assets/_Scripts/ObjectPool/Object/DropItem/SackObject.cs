using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SackObject : DropItemObject
{
    public int itemId;
    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
        itemId = -1;
    }
    public void SetItemId()
    {
        //百分之60几率掉药水
        if (Random.Range(0, 1f)<= 0.5f)
        {
            itemId = Random.Range(1, 3);
        }else
        {
            itemId = Random.Range(3, 9);
        }
    }
    public void SetItemId(int id)
    {
        //掉神器
        itemId = id;
    }

}
