using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPool : MonoBehaviour
{
    //集合
    List<GameObject> objects = new List<GameObject>();
    //预设
    GameObject prefab;
    //父物体的位置
    //Transform parent;

    //名字
    public string Name
    {
        get
        {
            return prefab.name;
        }
    }
    public SubPool(GameObject prefabGO)
    {
        this.prefab = prefabGO;
    }
    //取出物体
    public GameObject Spawn(Transform trans)//寻找数组中没有active的物体，把他返回
    {
        GameObject go = null;
        foreach (var obj in objects)
        {
            if (obj.activeSelf == false)
            {
                go = obj;
            }
        }
        if (go == null)//若没有未active物体就Instantiate一个
        {
            go = GameObject.Instantiate(prefab,trans);
            //go.transform.parent = trans;
            go.transform.localPosition = Vector3.zero;
            objects.Add(go);
        }
        go.SetActive(true);
        go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
        return go;
    }
    //回收物体
    public void UnSpawn(GameObject go)
    {
        if (Contain(go))//数组里包含这个物体的话才执行
        {
            go.SendMessage("OnUnSpawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }
    //回收所有
    public void UnSpawnAll()
    {
        foreach (var obj in objects)
        {
            if (obj.activeSelf)
            {
                UnSpawn(obj);
            }
        }
    }

    public bool Contain(GameObject go)
    {
        return objects.Contains(go);
    }
}
