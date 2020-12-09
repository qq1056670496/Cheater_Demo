using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInTrigger : MonoBehaviour
{
    private int goldNum=0;
    private List<GameObject> inRangeGoldList = new List<GameObject>();
    private List<GameObject> needRemoveGoldInDictList = new List<GameObject>();

    private List<int> inRangeItemIdList = new List<int>();

    private List<GameObject> inRangeItemGoList = new List<GameObject>();
    private List<int> needRemoveItemIndexInDictList = new List<int>();


    private PlayerState ps;
    private void Awake()
    {
        ps = GetComponentInParent<PlayerState>();
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ps.Money += goldNum;
            goldNum = 0;

            //处理物品
            for (int i = 0; i < inRangeItemIdList.Count; i++)
            {
                if (UIPanelManager.Instance.KnapsackPanel.HaveEmptySlot())
                {
                    UIPanelManager.Instance.KnapsackPanel.StoreItem(inRangeItemIdList[i]);
                    ObjectPoolManager._instance.UnSpawn(inRangeItemGoList[i]);
                    //needRemoveItemIndexInDictList.Add(i);

                    inRangeItemIdList.RemoveAt(i);
                    inRangeItemGoList.RemoveAt(i);
                }
                else
                {
                    WarningTip._instance.Show("背包空间不足");
                    break;
                }
            }
            //foreach (var i in needRemoveItemIndexInDictList)
            //{
            //    if (inRangeItemGoList.Count > i)//错误判断 只有长度足够才执行
            //    {
            //        inRangeItemIdList.RemoveAt(i);
            //        inRangeItemGoList.RemoveAt(i);
            //    }
            //}
            //needRemoveItemIndexInDictList.Clear();



            //清空gold
            foreach (GameObject go in inRangeGoldList)
            {
                ObjectPoolManager._instance.UnSpawn(go);
                needRemoveGoldInDictList.Add(go);
            }
            foreach (GameObject go in needRemoveGoldInDictList)
            {
                inRangeGoldList.Remove(go);
            }
            needRemoveGoldInDictList.Clear();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Gold")
        {
            goldNum += other.GetComponent<GoldObject>().goldNum;
            inRangeGoldList.Add(other.gameObject);
        }else if(other.tag == "Sack")
        {
            inRangeItemIdList.Add(other.GetComponent<SackObject>().itemId);
            inRangeItemGoList.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Gold")
        {
            goldNum += other.GetComponent<GoldObject>().goldNum;
            inRangeGoldList.Remove(other.gameObject);
        }
        else if (other.tag == "Sack")
        {
            inRangeItemIdList.Remove(other.GetComponent<SackObject>().itemId);
            inRangeItemGoList.Remove(other.gameObject);
        }

        
    }
}
