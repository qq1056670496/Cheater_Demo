using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class CharacterPanel : BasePanel
{
    private PlayerState playerState;
    private EquipmentSlot[] slots;

    private void Awake()
    {
        //_instance = this;
        //playerState = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        //slots = GetComponentsInChildren<EquipmentSlot>();
    }
    public override void Init()
    {
        playerState = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        slots = GetComponentsInChildren<EquipmentSlot>();
    }

    public void PutOn(Item item)
    {
        Item existItem = null;
        foreach (EquipmentSlot slot in slots)
        {
            EquipmentSlot equipmentSlot = (EquipmentSlot)slot;
            if (equipmentSlot.IsRightItem(item))//类型是否相符， 找到对应装备槽
            {
                if (equipmentSlot.transform.childCount > 0)//装备槽已经有装备，进行替换
                {
                    ItemUI currentItemUI = equipmentSlot.transform.GetComponentInChildren<ItemUI>();//找到的装备槽的ItemUI
                    existItem = currentItemUI.Item;//存储要取出的item
                    currentItemUI.SetItem(item, 1);
                }
                else
                //穿戴上
                {
                    equipmentSlot.StoreItem(item);
                }
                break;
            }
        }
        if (existItem != null)
            UIPanelManager.Instance.KnapsackPanel.StoreItem(existItem);

        UpdatePropertyText();
    }
    public void PutOff(Item item)
    {
        UIPanelManager.Instance.KnapsackPanel.StoreItem(item);
        UpdatePropertyText();
    }

    //更新角色的add属性
    public void UpdatePropertyText()
    {
        int strength = 0, intellect = 0, agility = 0, stamina = 0;
        float attack = 0;
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.transform.childCount > 0)
            {
                Item item = slot.transform.GetComponentInChildren<ItemUI>().Item;
                if (item is Equipment)
                {
                    Equipment e = (Equipment)item;
                    strength += e.Strength;
                    intellect += e.Intellect;
                    agility += e.Agility;
                    stamina += e.Stamina;
                }
                else if (item is Weapon)
                {
                    Weapon w = (Weapon)item;
                    attack += w.Attack;
                }
            }
        }
        playerState.addStrength = strength;
        playerState.addIntellect = intellect;
        playerState.addAgility = agility;
        playerState.addStamina = stamina;
        playerState.addAttack = attack;
        playerState.UpdateStates();
        UIPanelManager.Instance.PropertyPanel.UpdatePropertyPanel();
    }
    //public override void ShowOrHide()
    //{
    //    if (isShow)
    //    {
    //        InventoryManager.Instance.HideToolTip();
    //        gameObject.SetActive(false);
    //        isShow = false;
    //    }
    //    else
    //    {
    //        gameObject.SetActive(true);
    //        isShow = true;
    //    }
    //}
    public override void Hide()
    {
        InventoryManager.Instance.HideToolTip();
        base.Hide();
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
