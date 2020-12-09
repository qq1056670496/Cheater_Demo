using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopPanel : Inverntory
{
    public Item buyItem=null;
    public GameObject BuyTip;
    private Text buyTipText;
    private PlayerState ps;
    private PlayerMove pm;
    private void Awake()
    {
        //slots = GetComponentsInChildren<ShopSlot>();
        //buyTip = GameObject.Find("BuyTip");
        //buyTipText = buyTip.transform.Find("InfoText").GetComponent<Text>();
        //ps = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        //pm = ps.GetComponent<PlayerMove>();
        //HideBuyTip();
    }
    private void Start()
    {
        //for (int i = 1; i < 9; i++)
        //{
        //    if (i == 1 || i == 2)
        //        StoreItem(i, 99);
        //    else
        //    {
        //        StoreItem(i);
        //    }
        //}
    }
    public override void Init()
    {
        slots = GetComponentsInChildren<ShopSlot>();
        buyTipText = BuyTip.transform.Find("InfoText").GetComponent<Text>();
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        pm = ps.GetComponent<PlayerMove>();
        HideBuyTip();

        
    }
    public void StartInit()
    {
        for (int i = 1; i < 9; i++)
        {
            if (i == 1 || i == 2)
                StoreItem(i, 99);
            else
            {
                StoreItem(i);
            }
        }
    }
    public void ShowBuyTip(string text)
    {
        BuyTip.SetActive(true);
        buyTipText.text = text;
    }
    public void HideBuyTip()
    {
        BuyTip.SetActive(false);
    }
    public void BuyItem()
    {
        if(buyItem!=null)
        {
            if( ps.UseMoney(buyItem.BuyPrice))
            {
                UIPanelManager.Instance.KnapsackPanel.GetComponent<Knapsack>().StoreItem(buyItem);
                WarningTip._instance.Show("购买成功！");
            }
            else
            {
                WarningTip._instance.Show("金币不足,无法购买！");
            }
        }
        HideBuyTip();
    }

    public override void Show()
    {
        //pm.agent.isStopped = true;
        pm.CanMove = false;
        pm.anim.SetBool("Walk", false);
        base.Show();
    }
    public override void Hide()
    {
        InventoryManager.Instance.HideToolTip();
        base.Hide();

    }
}
