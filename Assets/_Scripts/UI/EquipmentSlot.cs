using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    public Equipment.EquipmentType equipType;



    public override void OnPointerDown(PointerEventData eventData)
    {

        if (!InventoryManager.Instance.pickedItem.gameObject.activeInHierarchy && eventData.button == PointerEventData.InputButton.Right)//右键点击卸下装备
        {
            if (transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetComponentInChildren<ItemUI>();
                Item tempItem = currentItemUI.Item;
                DestroyImmediate(currentItemUI.gameObject);
                transform.parent.SendMessage("PutOff", tempItem);
                ToolTip._instance.Hide();
            }
        }

        if (eventData.button != PointerEventData.InputButton.Left) return;
        //手上有东西
        //与物品槽类型是否相符
        //装备槽有没有东西
        //手上没东西
        //装备槽没有东西

        if (InventoryManager.Instance.pickedItem.gameObject.activeInHierarchy)//手上有东西
        {
            Debug.Log("手上有东西");
            ItemUI pickedItemUI = InventoryManager.Instance.pickedItem;
            //判断类型是否相符
            if (IsRightItem(pickedItemUI.Item))
            {
                if (transform.childCount > 0)//装备槽有东西 交换
                {
                    ItemUI currentItemUI = GetComponentInChildren<ItemUI>();
                    Item tempItem = currentItemUI.Item;
                    int amount = currentItemUI.Amount;
                    currentItemUI.SetItem(pickedItemUI.Item, pickedItemUI.Amount);
                    pickedItemUI.SetItem(tempItem, amount);
                }
                else//装备槽没有东西 放下装备
                {
                    StoreItem(pickedItemUI.Item);
                    pickedItemUI.gameObject.SetActive(false);
                }
            }//不相符不做处理
        }
        else//手上没东西
        {
            //if (transform.childCount > 0)//拿起
            //{
            //    Debug.Log("拿起");
            //    ItemUI currentItemUI = GetComponentInChildren<ItemUI>();
            //    InventoryManager.Instance.PickUpItem(currentItemUI.Item, currentItemUI.Amount);
            //    DestroyImmediate(currentItemUI.gameObject);
            //}
        }
        UIPanelManager.Instance.CharacterPanel.UpdatePropertyText();
    }
    //类型是否相符合
    public bool IsRightItem(Item item)
    {
        return (item is Weapon&& this.equipType==Equipment.EquipmentType.None) ||
                (item is Equipment && ((Equipment)(item)).EquipType == this.equipType);
    }
}
