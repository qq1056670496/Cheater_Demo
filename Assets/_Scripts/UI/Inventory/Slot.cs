using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    public GameObject itemPrefab;

    //没有子物品就新建，有则amount++
    public void StoreItem(Item item,int amount=1)
    {
        if (transform.childCount == 0)
        {
            GameObject itemGO = Instantiate(itemPrefab,transform,false);
            //itemGO.transform.SetParent(transform);
            itemGO.transform.localPosition = Vector3.zero;
            itemGO.GetComponent<ItemUI>().SetItem(item,amount);
        }
        else
        {
            transform.GetComponentInChildren<ItemUI>().AddAmount();
        }
    }
    public void StoreItem(int id)
    {
        StoreItem(InventoryManager.Instance.GetItemById(id));
    }
    public int GetItemId()
    {
        return transform.GetComponentInChildren<ItemUI>().Item.ID;
    }
    public bool IsFilled()
    {
        ItemUI itemUI = transform.GetComponentInChildren<ItemUI>();
        return itemUI.Amount >= itemUI.Item.Capacity;//当前数量大于等于容量
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            string showText = GetComponentInChildren<ItemUI>().Item.Name;
            InventoryManager.Instance.ShowToolTip(GetComponentInChildren<ItemUI>().Item.GetToolTipText());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.HideToolTip();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!InventoryManager.Instance.pickedItem.gameObject.activeInHierarchy && eventData.button == PointerEventData.InputButton.Right)//右键点击穿戴装备
        {
            if (transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetComponentInChildren<ItemUI>();
                if (currentItemUI.Item is Equipment || currentItemUI.Item is Weapon)
                {
                    currentItemUI.AddAmount(-1);
                    Item tempItem = currentItemUI.Item;
                    if (currentItemUI.Amount <= 0)
                    {
                        DestroyImmediate(currentItemUI.gameObject);
                    }
                    UIPanelManager.Instance.CharacterPanel.PutOn(tempItem);
                    ToolTip._instance.Hide();
                }
            }
        }

        //处理左右键都能拿起
        if (eventData.button != PointerEventData.InputButton.Left) return;

        // 处理点击物品槽
        if (transform.childCount > 0)//物品槽不为空
        {
            ItemUI currentItemUI = transform.GetComponentInChildren<ItemUI>();
            if (InventoryManager.Instance.pickedItem.gameObject.activeInHierarchy == false)//没有被选择的Item
            {
                if (Input.GetKey(KeyCode.LeftAlt))//取得一半
                {
                    int amountPicked = (currentItemUI.Amount + 1) / 2;
                    InventoryManager.Instance.PickUpItem(currentItemUI.Item, amountPicked);
                    int amountRemained = currentItemUI.Amount - amountPicked;
                    if (amountRemained <= 0)
                    {
                        Destroy(currentItemUI.gameObject);
                    }
                    else
                    {
                        currentItemUI.SetAmount(amountRemained);
                        
                    }
                }
                else//取得所有
                {
                    InventoryManager.Instance.PickUpItem(currentItemUI.Item, currentItemUI.Amount);
                    Destroy(currentItemUI.gameObject);

                }
            }
            else//有pickedItem
            {
                if (currentItemUI.Item.ID == InventoryManager.Instance.pickedItem.Item.ID)//与槽内ID相等
                {
                    if (Input.GetKey(KeyCode.LeftAlt))//按下alt一个一个放
                    {
                        if (currentItemUI.Item.Capacity > currentItemUI.Amount)//容量没满
                        {
                            currentItemUI.AddAmount();//物品槽内数量+1
                            //pickedItem-1
                            InventoryManager.Instance.RemovePickedUpItem();
                        }
                    }
                    else//没有按下alt
                    {
                        if (currentItemUI.Item.Capacity > currentItemUI.Amount)
                        {
                            int capacityRemained = currentItemUI.Item.Capacity - currentItemUI.Amount;
                            if (capacityRemained >= InventoryManager.Instance.pickedItem.Amount)//全部放下
                            {
                                currentItemUI.AddAmount(InventoryManager.Instance.pickedItem.Amount);
                                InventoryManager.Instance.RemovePickedUpItem(InventoryManager.Instance.pickedItem.Amount);
                            }
                            else
                            {
                                currentItemUI.AddAmount(capacityRemained);
                                InventoryManager.Instance.RemovePickedUpItem(capacityRemained);
                            }
                        }
                    }
                }
                else//id不同进行交换
                {
                    Item item = currentItemUI.Item;
                    int amount = currentItemUI.Amount;
                    currentItemUI.SetItem(InventoryManager.Instance.pickedItem.Item, InventoryManager.Instance.pickedItem.Amount);
                    InventoryManager.Instance.pickedItem.SetItem(item, amount);
                }
            }
        }
        else
        {
            //物品槽为空  
            //  1,pickedItem!=null
            //1,按下alt键，  放置鼠标上物品的一个
            //2，不按alt       放置鼠标上的所有

            //  2,pickedItem==null
            //不做处理

            if (InventoryManager.Instance.pickedItem.gameObject.activeInHierarchy)//手上有东西
            {
                if (Input.GetKey(KeyCode.LeftAlt))//一个一个放
                {
                    this.StoreItem(InventoryManager.Instance.pickedItem.Item);
                    InventoryManager.Instance.RemovePickedUpItem();
                }
                else//全部放下
                {
                    for (int i = 0; i < InventoryManager.Instance.pickedItem.Amount; i++)
                    {
                        this.StoreItem(InventoryManager.Instance.pickedItem.Item);
                    }
                    InventoryManager.Instance.RemovePickedUpItem(InventoryManager.Instance.pickedItem.Amount);
                }

            }
        }
    }




}
