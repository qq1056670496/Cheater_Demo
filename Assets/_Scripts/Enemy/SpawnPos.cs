using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPos : MonoBehaviour
{
    //[SerializeField]private bool isAllSpawnOrActive = false;
    public int maxNum=0;
    public string PrefabName="";
    //private bool hasGetChildList = false;
    //[SerializeField]private EnemyObject[] childList;

    //public float spawnTime = 3;
    //[SerializeField]private float spawnTimer;
    //private int childCount = 0;

    private void Start()
    {
        for (int i = 0; i < maxNum; i++)
        {
            GameObject tempGo= Instantiate( Resources.Load("Enemies/"+PrefabName) as GameObject , transform);
            tempGo.transform.position += new Vector3(Random.Range(-5, 5.0f), 0, Random.Range(-5, 5.0f));
        }
    }
    private void Update()
    {
        //SetIsAllSpawn();
        
        //if (!isAllSpawnOrActive)
        //{
        //    spawnTimer += Time.deltaTime;
        //    if (spawnTimer >= spawnTime)
        //    {
        //        Spawn();
        //        spawnTimer = 0;
        //    }
        //}
    }
    //private void Spawn()
    //{
    //    childCount++;
    //    ObjectPoolManager._instance.Spawn(PrefabName, transform);
    //}
    //private void SetIsAllSpawn()
    //{
    //    if (maxNum != 0)
    //    {
    //        if (childCount < maxNum)
    //        {
    //            isAllSpawnOrActive = false;
    //        }
    //        else
    //        {
    //            childList = GetComponentsInChildren<EnemyObject>();

    //            if (childList.Length < maxNum)
    //            {
    //                isAllSpawnOrActive = false; return;
    //            }
    //            isAllSpawnOrActive = true;
    //        }
    //    }
    //}

}
