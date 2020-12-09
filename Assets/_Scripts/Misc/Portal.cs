using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string SceneNameFrom;
    public string SceneNameTo;
    private Transform _dropItemParent;
    private void Awake()
    {
        _dropItemParent = GameObject.Find("DropItems").transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DropItemObject[] items = _dropItemParent.GetComponentsInChildren<DropItemObject>();
            if (items.Length > 0)
            {
                foreach (var item in items)
                {
                    if (item.gameObject.activeInHierarchy == true)
                    {
                        ObjectPoolManager._instance.UnSpawn(item.gameObject);
                    }
                }
            }

            Destroy(GameObject.FindWithTag("Env"));
            Instantiate(Resources.Load(SceneNameTo));
            Loading.Instance.OnLoading();
            other.transform.position = Vector3.zero;
        }
    }
}
