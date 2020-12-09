using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : Item
{
    public int HP { get; set; }
    public int MP { get; set; }
    public Medicine(int id, string name, ItemType type, int buyPrice, int sellPirce, string sprite, ItemQuality quality,int capacity,int hp,int mp) 
        : base(id, name, type, buyPrice, sellPirce, sprite, quality,capacity)
    {
        this.HP = hp;
        this.MP = mp;
    }

    public override string GetToolTipText()
    {
        string text= base.GetToolTipText();

        if (HP > 0)
        {
            text += "\n加血：" + HP.ToString();
        }
        if (MP > 0)
        {
            text += "\n加蓝：" + MP.ToString();
        }
        return text;
    }
}
