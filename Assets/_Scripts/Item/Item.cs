using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public string Sprite { get; set; }
    public ItemQuality Quality { get; set; }
    public int Capacity { get; set; }
    public Item()
    {
        this.ID = -1;
    }
    public Item(int id,string name,ItemType type,int buyPrice,int sellPirce,string sprite,ItemQuality quality,int capacity)
    {
        this.ID = id;
        this.Name = name;
        this.Type = type;
        this.BuyPrice = buyPrice;
        this.SellPrice = sellPirce;
        this.Sprite = sprite;
        this.Quality = quality;
        this.Capacity = capacity;
    }
    public virtual string GetToolTipText()
    {
        string color = "";
        switch (Quality)
        {
            case ItemQuality.Commom:
                color = "white";
                break;
            case ItemQuality.Rare:
                color = "blue";
                break;
            case ItemQuality.Epic:
                color = "orange";
                break;
            case ItemQuality.Legendary:
                color = "red";
                break;
            default:
                break;
        }
        string text = string.Format("<color={0}>{1}</color>\n购买价格:{2}\n出售价格{3}", color, Name, BuyPrice, SellPrice);
        return text;
    }
    public enum ItemType
    {
        Medicine,
        Weapon,
        Equipment
    }
    public enum ItemQuality
    {
        Commom,
        Rare,
        Epic,
        Legendary
    }

}

