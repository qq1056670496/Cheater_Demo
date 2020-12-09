using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemObject : ReusableObject
{
    public override void OnSpawn()
    {

    }

    public override void OnUnSpawn()
    {
        SoundManager._instance.PlayAudio("PickUpItem");
    }

}
