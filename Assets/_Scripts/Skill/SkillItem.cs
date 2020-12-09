using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillItem : MonoBehaviour
{
    public int id;
    public int skillLevel;
    private Text desText;
    private Image iconImage;
    private GameObject iconMask;
    private void Awake()
    {
        InitSkillItem();
    }
    private void InitSkillItem()
    {
        if (iconMask == null)
        {
            desText = transform.Find("Des").GetComponent<Text>();
            iconImage = transform.Find("Icon").GetComponent<Image>();
            iconMask = transform.Find("Mask").gameObject;
        }
    }
    public void SetId(int id)
    {
        InitSkillItem();
        this.id = id;
        SkillInfo info = SkillManager._instance.GetSkillInfoById(id);
        skillLevel = info.level;
        desText.text = info.description;
        iconImage.sprite = Resources.Load(info.sprite, typeof(Sprite)) as Sprite;
    }
    public void UpdateShow(int level)//传入ps.level
    {
        if (iconMask == null)
        {
            desText = transform.Find("Des").GetComponent<Text>();
            iconImage = transform.Find("Icon").GetComponent<Image>();
            iconMask = transform.Find("Mask").gameObject;
        }
        if (skillLevel <= level)
        {
            iconMask.SetActive(false);
            //iconName_image.GetComponent<SkillIconName>().enabled = true;
        }
        else
        {
            iconMask.SetActive(true);
            //iconName_image.GetComponent<SkillIconName>().enabled = false;
        }
    }

}
