using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemUI : MonoBehaviour
{
    public Item Item { get; set; }
    private int amount;
    public int Amount
    {
        get { return amount; }
        set
        {
            amount = value;
            AmountText.text = Amount.ToString();
        }
    }
    private Image itemImage;
    private Text amountText;
    private Image ItemImage
    {
        get
        {
            if (itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
            return itemImage;
        }
    }
    private Text AmountText
    {
        get
        {
            if (amountText == null)
            {
                amountText = GetComponentInChildren<Text>();
            }
            return amountText;
        }
    }
    public void SetItem(Item item, int amount = 1)
    {
        transform.localScale = Vector3.one;
        this.Item = item;
        this.Amount = amount;
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        if (item.Capacity > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }
    public void AddAmount(int amount = 1)
    {
        //transform.localScale = Vector3.one * 1.4f;
        Amount += amount;
        AmountText.text = Amount.ToString();
    }
    public void SetAmount(int amount)
    {
        //transform.localScale = Vector3.one * 1.4f;
        this.Amount = amount;
        AmountText.text = Amount.ToString();
    }
    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }


}
