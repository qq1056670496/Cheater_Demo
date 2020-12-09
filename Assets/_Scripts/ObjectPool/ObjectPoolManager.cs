using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager _instance;
    private void Awake()
    {
        _instance = this;
    }

    //资源目录
    public string ResourcesDir = "Objects";
    //子池子的字典
    Dictionary<string, SubPool> pools = new Dictionary<string, SubPool>();

    //取出物体
    public GameObject Spawn(string name, Transform trans)//预制体名字和该物体父亲位置
    {
        SubPool pool = null;
        if (!pools.ContainsKey(name))//如果名字不在子池子字典中，新建一个子池子
        {
            RegisterNew(name);
        }
        pool = pools[name];//取得该子池子
        return pool.Spawn(trans);
    }


    //回收物体
    public void UnSpawn(GameObject go)
    {
        SubPool pool = null;
        foreach (var p in pools.Values)//遍历子池子，找到包含go的子池子
        {
            if (p.Contain(go))
            {
                pool = p;
                break;
            }
        }
        pool.UnSpawn(go);
    }


    //回收所有物体
    public void UnSpawnAll()
    {
        foreach (var p in pools.Values)//调用所有子池子的UnSpawnAll方法
        {
            p.UnSpawnAll();
        }
    }
    //清除所有对象池的物体
    public void Clear()
    {
        pools.Clear();
    }



    //新建子池子
    public void RegisterNew(string names)
    {
        //资源目录
        string path = ResourcesDir + "/" + names;
        //生成预制体
        GameObject go = Resources.Load<GameObject>(path);
        //新建子池子
        SubPool pool = new SubPool(go);
        pools.Add(pool.Name, pool);
    }
}
