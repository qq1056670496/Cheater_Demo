using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSlot : Slot
{
    private ShopPanel shopPanel;
    private void Awake()
    {
        
        shopPanel = GetComponentInParent<ShopPanel>();
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (transform.childCount < 1) return;
        //!InventoryManager.Instance.pickedItem.gameObject.activeInHierarchy &&
        if (eventData.button == PointerEventData.InputButton.Right)//右键点击弹出购买框
        {
            Item tempItem= GetComponentInChildren<ItemUI>().Item; ;
            shopPanel.buyItem = tempItem;
            shopPanel.ShowBuyTip("购买将花费："+tempItem.BuyPrice+"\n是否确认？");
        }
    }
}
