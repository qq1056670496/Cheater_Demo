using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ReusableObject
{
    public override void OnSpawn()
    {
        StartCoroutine(OnUnSpawnTimer());
    }

    public override void OnUnSpawn()
    {
        StopAllCoroutines();
    }

    IEnumerator OnUnSpawnTimer()
    {
        yield return new WaitForSeconds(2);
        ObjectPoolManager._instance.UnSpawn(gameObject);
    }
}
