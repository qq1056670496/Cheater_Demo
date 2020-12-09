using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int Attack { get; set; }
    public Weapon(int id, string name, ItemType type, int buyPrice, int sellPirce, string sprite, ItemQuality quality, int capacity, int attack) 
        : base(id, name, type, buyPrice, sellPirce, sprite, quality,capacity)
    {
        this.Attack = attack;
    }

    public override string GetToolTipText()
    {
        string text= base.GetToolTipText();
        string newText = string.Format("{0}\n攻击力:{1}", text, Attack);
        return newText;
    }

}
