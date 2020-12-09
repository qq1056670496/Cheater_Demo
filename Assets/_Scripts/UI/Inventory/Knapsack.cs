using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class Knapsack : Inverntory
{
    private Text moneyText;

    private void Awake()
    {
        //_instance = this;
        //slots = GetComponentsInChildren<Slot>();
        //moneyText = transform.Find("MoneyPanel/MoneyText").GetComponent<Text>();
    }
    public override void Init()
    {
        slots = GetComponentsInChildren<Slot>();
        moneyText = transform.Find("MoneyPanel/MoneyText").GetComponent<Text>();
    }

    public int GetNumById(int id)
    {
        foreach (var slot in slots)
        {
            if (slot.transform.childCount > 0 && slot.GetComponentInChildren<ItemUI>().Item.ID == id)
            {
                return slot.GetComponentInChildren<ItemUI>().Amount;
            }
        }
        return 0;
    }
    public bool ReduceById(int id,int amount=1)
    {
        foreach (var slot in slots)
        {
            if (slot.transform.childCount > 0 && slot.GetComponentInChildren<ItemUI>().Item.ID == id)
            {
                slot.GetComponentInChildren<ItemUI>().Amount-=amount;
                if (slot.GetComponentInChildren<ItemUI>().Amount == 0)
                {
                    Destroy(slot.transform.GetChild(0).gameObject);
                }
                return true;
            }
        }

        return false;
    }
    public void UpdateMoneyText(int money)
    {
        moneyText.text = money.ToString();
    }



}
