using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class InventoryManager : MonoBehaviour
{
    public List<Item> itemList;
    private Canvas canvas;
    private Vector2 toolTipOffset = new Vector2(33, -26);
    private bool isToolTipShow = false;
    public ItemUI pickedItem;
    [SerializeField]private GameObject _throwTip;
    #region 单例模式
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Managers").GetComponent<InventoryManager>(); 
            }
            return _instance;
        }
    }

    #endregion


    private void Awake()
    {
        InitItemInfo();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        pickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();
        pickedItem.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (pickedItem.gameObject.activeInHierarchy)//捡起了物品的时候 物品跟随鼠标移动
        {
            Vector2 positionPickedItem;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform.GetComponent<RectTransform>(), Input.mousePosition, null, out positionPickedItem);
            pickedItem.SetLocalPosition(positionPickedItem);
        }
        else if (isToolTipShow)//tooltip跟随鼠标
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform.GetComponent<RectTransform>(), Input.mousePosition, null, out position);
            ToolTip._instance.SetPosition(position + toolTipOffset);
        }

        if (pickedItem.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        //检测是否在UI上  丢
        {
            _throwTip.SetActive(true);
            
        }
    }
    public void Throw()//丢
    {
        pickedItem.gameObject.SetActive(false);
    }

    public void ShowToolTip(string content)
    {
        if (pickedItem.gameObject.activeInHierarchy) return;
        isToolTipShow = true;
        ToolTip._instance.Show(content);
    }
    public void HideToolTip()
    {
        isToolTipShow = false;
        ToolTip._instance.Hide();
    }
    public Item GetItemById(int id)
    {
        foreach (var item in itemList)
        {
            if (item.ID == id)
                return item;
        }
        return null;
    }
    private void InitItemInfo()
    {
        itemList = new List<Item>();
        TextAsset itemText = Resources.Load<TextAsset>("Items");
        string itemsJson = itemText.text;
        JsonData j = JsonMapper.ToObject(itemsJson);
        foreach (JsonData temp in j)
        {
            Item itemTemp = new Item();
            int id = int.Parse(temp["id"].ToString());
            string name = temp["name"].ToString();
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), temp["type"].ToString());
            Item.ItemQuality quality = (Item.ItemQuality)System.Enum.Parse(typeof(Item.ItemQuality), temp["quality"].ToString());
            int buyPrice = int.Parse(temp["buyPrice"].ToString());
            int sellPrice = int.Parse(temp["sellPrice"].ToString());
            int capacity = int.Parse(temp["capacity"].ToString());
            string sprite = temp["sprite"].ToString();
            switch (type)
            {
                case Item.ItemType.Medicine:
                    int hp = int.Parse(temp["hp"].ToString());
                    int mp = int.Parse(temp["mp"].ToString());
                    itemTemp = new Medicine(id, name, type, buyPrice, sellPrice, sprite, quality,capacity, hp, mp);
                    break;
                case Item.ItemType.Weapon:
                    int attack = int.Parse(temp["attack"].ToString());
                    itemTemp = new Weapon(id, name, type, buyPrice, sellPrice, sprite, quality, capacity, attack);
                    break;
                case Item.ItemType.Equipment:
                    int strength = int.Parse(temp["strength"].ToString());
                    int intellect = int.Parse(temp["intellect"].ToString());
                    int agility = int.Parse(temp["agility"].ToString());
                    int stamina = int.Parse(temp["stamina"].ToString());
                    Equipment.EquipmentType equipType = (Equipment.EquipmentType)System.Enum.Parse(typeof(Equipment.EquipmentType), temp["equipType"].ToString());
                    itemTemp = new Equipment(id, name, type, buyPrice, sellPrice, sprite, quality, capacity, strength,intellect,agility,stamina,equipType);
                    break;
                default:
                    break;
            }

            itemList.Add(itemTemp);
        }
    }


    //捡起物品槽中的所有数量物品
    public void PickUpItem(ItemUI itemUI)
    {
        pickedItem.SetItem(itemUI.Item, itemUI.Amount);
        pickedItem.gameObject.SetActive(true);
        Vector2 positionPickedItem;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform.GetComponent<RectTransform>(), Input.mousePosition, null, out positionPickedItem);
        pickedItem.SetLocalPosition(positionPickedItem);
    }
    //捡起指定数量物品
    public void PickUpItem(Item item, int amount)
    {
        pickedItem.SetItem(item, amount);
        pickedItem.gameObject.SetActive(true);
        ToolTip._instance.Hide();
    }
    //从手上拿掉物品
    public void RemovePickedUpItem(int amount = 1)
    {
        pickedItem.AddAmount(-amount);
        if (pickedItem.Amount <= 0)
        {
            pickedItem.gameObject.SetActive(false);
        }
    }
}
