using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }
    public EquipmentType EquipType { get; set; }
    public Equipment(int id, string name, ItemType type, int buyPrice, int sellPirce, string sprite, ItemQuality quality, int capacity,
        int strength,int intellect,int agility,int stamina,EquipmentType equipType) 
        : base(id, name, type, buyPrice, sellPirce, sprite, quality,capacity)
    {
        this.Strength = strength;
        this.Intellect = intellect;
        this.Agility = agility;
        this.Stamina = stamina;
        this.EquipType = equipType;
    }

    public override string GetToolTipText()
    {
        string equipType = "";
        switch (EquipType)
        {
            case EquipmentType.Helmet:
                equipType = "头部";
                break;

            case EquipmentType.Chest:
                equipType = "护甲";
                break;
            case EquipmentType.Pants:
                equipType = "腿部";
                break;
            case EquipmentType.Gloves:
                equipType = "手部";
                break;
            case EquipmentType.Boots:
                equipType = "鞋子";
                break;
            default:
                break;
        }
        string text = base.GetToolTipText();
        text += "\n" + "装备类型:" + equipType;
        if (Strength > 0)
            text += "\n"+"力量:" + Strength ;
        if (Intellect > 0)
            text += "\n" + "智慧:" + Intellect ;
        if (Agility > 0)
            text += "\n" + "敏捷:" + Agility ;
        if (Stamina > 0)
            text += "\n" + "耐力:" + Stamina ;

        return text;
    }
    public enum EquipmentType
    {
        None,
        Helmet,
        Chest,
        Gloves,
        Pants,
        Boots
    }
}
