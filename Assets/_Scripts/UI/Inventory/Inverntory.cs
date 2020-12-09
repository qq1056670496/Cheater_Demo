using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Inverntory : BasePanel
{
    protected Slot[] slots;
    protected bool isShow = true;
    public override void Init()
    {
    }
    public bool StoreItem(int id,int amount=1)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item,amount);
    }
    public bool StoreItem(Item item,int amount=1)
    {
        if (item == null)
        {
            Debug.LogWarning("存储的物品id不存在 id:");
            return false;
        }
        if (item.Capacity == 1)//堆叠数量为1
        {
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.LogWarning("没有空物品槽");
                return false;
            }
            else
            {
                slot.StoreItem(item);
                return true;
            }
        }
        else//可多数量堆叠的
        {
            Slot slot = FindSameIdSlot(item);//寻找同Id的物品
            if (slot != null)
            {
                if(amount+slot.GetComponentInChildren<ItemUI>().Amount> slot.GetComponentInChildren<ItemUI>().Item.Capacity)//叠加后超过容量
                {
                    slot.StoreItem(item, slot.GetComponentInChildren<ItemUI>().Item.Capacity- slot.GetComponentInChildren<ItemUI>().Amount);
                    Slot emptySlot = FindEmptySlot();
                    if (emptySlot != null)
                    {
                        emptySlot.StoreItem(item,amount-(slot.GetComponentInChildren<ItemUI>().Item.Capacity - slot.GetComponentInChildren<ItemUI>().Amount));
                        return true;
                    }
                    else
                    {
                        Debug.LogWarning("没有空物品槽");
                        return false;
                    }
                }
                else
                {
                    slot.StoreItem(item,amount);
                    return true;
                }
            }
            else
            {
                Slot emptySlot = FindEmptySlot();
                if (emptySlot != null)
                {
                    emptySlot.StoreItem(item,amount);
                    return true;
                }
                else
                {
                    Debug.LogWarning("没有空物品槽");
                    return false;
                }
            }
        }
    }

    protected Slot FindEmptySlot()
    {
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }
    protected Slot FindSameIdSlot(Item item)
    {
        foreach (var slot in slots)
        {
            if (slot.transform.childCount >= 1 && slot.GetItemId() == item.ID)
            {
                return slot;
            }
        }
        return null;
    }

    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        InventoryManager.Instance.HideToolTip();
        base.Hide();

    }


    public Item GetItemById(int id)
    {
        foreach (var slot in slots)
        {
            if (slot.transform.childCount > 0 && slot.GetComponentInChildren<ItemUI>().Item.ID == id)
            {
                return slot.GetComponentInChildren<ItemUI>().Item;
            }
        }
        return null;
    }
    
    public bool HaveEmptySlot()
    {
        Slot slot=FindEmptySlot();
        if (slot != null)
        {
            return true;
        }
        return false;
    }

    #region SaveAndLoad

    public void Save()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount > 0)
            {
                ItemUI currentItemUI = slot.transform.GetComponentInChildren<ItemUI>();
                sb.Append(currentItemUI.Item.ID + "," + currentItemUI.Amount + "-");
            }
            else
            {
                sb.Append("0-");
            }
        }
        PlayerPrefs.SetString(this.gameObject.name, sb.ToString());
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(this.gameObject.name) == false) return;//没有对应名字的数据
        string str = PlayerPrefs.GetString(this.gameObject.name);
        string[] itemArray = str.Split('-');
        for (int i = 0; i < itemArray.Length - 1; i++)//数字的i项就对应第i个slot
        {
            string itemStr = itemArray[i];
            if (itemStr != "0")
            {
                string[] temp = itemStr.Split(',');
                int id = int.Parse(temp[0]);
                int amount = int.Parse(temp[1]);
                Item item = InventoryManager.Instance.GetItemById(id);
                //存入amount数量的Item
                slots[i].StoreItem(item, amount);
            }
        }
    }
    #endregion
}
