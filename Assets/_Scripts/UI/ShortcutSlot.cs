using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;

public enum ShortCutType
{
    Skill,
    Medicine,
    None
}


public class ShortcutSlot : MonoBehaviour,IPointerDownHandler
{
    public KeyCode keycode;
    private ShortCutType type = ShortCutType.None;
    private SkillInfo skillInfo;
    private Medicine medicine;
    private int id;
    private Image icon;
    private PlayerState ps;
    private PlayerAttack pa;
    private PlayerMove pm;
    private int num = 0;
    private Image mask;


    private float coldTime;
    public float coldTimer=0;
    private void Awake()
    {
        icon =transform.Find("Image").GetComponent<Image>();
        icon.enabled = false;
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        pa = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        pm= GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        mask = transform.Find("Image/Mask").GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keycode))
        {
            if (type == ShortCutType.Medicine)//类型为药品
            {
                OnMedicineUse();
            }
            else if (type == ShortCutType.Skill&&coldTimer<=0&&!pa.isUsingSkill&&pm.canUseSkil)//技能
            {
                bool success = ps.TakeMp(skillInfo.mp);
                if (success)//蓝足够释放技能
                {
                    coldTimer = coldTime;
                    pa.UseSkill(skillInfo,this);
                }
                else
                {
                    WarningTip._instance.Show("蓝量不足");
                }
            }
        }
        if (type == ShortCutType.Medicine)
        {
            num = UIPanelManager.Instance.KnapsackPanel.GetNumById(id);
        }
        if (type == ShortCutType.Skill&&coldTime>0)
        {
            if ((skillInfo.skillType == SkillType.Position&&pa.isLockedPosition)||
                (skillInfo.skillType == SkillType.Direction && pa.isLockedDirection))//技能在锁定的时候不进行冷却
            {
                return;
            }
            else
            {
                coldTimer -= Time.deltaTime;
                mask.fillAmount = coldTimer / coldTime;
            }
        }
    }
    public void SetSkill(int id)
    {
        this.skillInfo = SkillManager._instance.GetSkillInfoById(id);
        this.id = id;
        icon.sprite = Resources.Load(skillInfo.sprite, typeof(Sprite)) as Sprite;
        type = ShortCutType.Skill;
        icon.enabled = true;
        coldTime = skillInfo.coldTime;
    }
    public void SetInventory(int id)
    {
        this.id = id;
        Item temp = InventoryManager.Instance.GetItemById(id) ;
        if (temp.Type==Item.ItemType.Medicine)
        {
            medicine = temp as Medicine;
            icon.sprite = Resources.Load(medicine.Sprite, typeof(Sprite)) as Sprite;
            type = ShortCutType.Medicine;
            icon.enabled = true;
        }
    }
    private void OnMedicineUse()
    {
        Item item = UIPanelManager.Instance.KnapsackPanel.GetItemById(id);
        if (item != null)
        {
            UIPanelManager.Instance.KnapsackPanel.ReduceById(id);
            ps.GetMedicine(medicine.HP,medicine.MP);
            if (num == 1)//
            {
                id = 0;
                icon.enabled = false;
                type = ShortCutType.None;
                //skillInfo = null;
                medicine = null;
                num = 0;
            }
        }
    }
    //只给PickedItem设置shortcut的时候用
    public void OnPointerDown(PointerEventData eventData)
    {
        if (InventoryManager.Instance.pickedItem.gameObject.activeInHierarchy)
        {
            SetInventory(InventoryManager.Instance.pickedItem.Item.ID);
            UIPanelManager.Instance.KnapsackPanel.StoreItem(InventoryManager.Instance.pickedItem.Item.ID, InventoryManager.Instance.pickedItem.Amount);
            InventoryManager.Instance.pickedItem.gameObject.SetActive(false);
        }

        //if (eventData.pointerEnter == true)
        //{

        //    GameObject surface = eventData.pointerEnter.gameObject;
        //    if (surface != null && surface.tag == "Shortcut")
        //    {
        //        surface.GetComponent<ShortcutSlot>().SetInventory(Item.ID);
        //    }
        //    else if (surface != null && surface.transform.parent.tag == "Shortcut")
        //    {
        //        surface.transform.parent.GetComponent<ShortcutSlot>().SetSkill(Item.ID);
        //    }
        //}
    }

    public void Save()
    {
        PlayerPrefs.SetInt(gameObject.name, id);
    }
    public void Load()
    {
        int id = PlayerPrefs.GetInt(gameObject.name);
        if (id == 0) return;
        if (id<=1000)//是药品
        {
            SetInventory(id);
        }
        else
        {
            SetSkill(id);
        }
    }
}
