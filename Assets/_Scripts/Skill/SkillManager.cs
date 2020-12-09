using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class SkillManager : MonoBehaviour
{
    public static SkillManager _instance;
    public Dictionary<int, SkillInfo> skilllDict = new Dictionary<int, SkillInfo>();
    private void Awake()
    {
        _instance = this;
        InitSkillInfoDict();
    }

    public SkillInfo GetSkillInfoById(int id)
    {
        SkillInfo info = null;
        if (skilllDict.TryGetValue(id, out info))
        {
            return info;
        }
        Debug.LogError("没有找到对应id的技能，id：" + id);
        return null;
    }

    //初始化技能字典
    private void InitSkillInfoDict()
    {
        TextAsset itemText = Resources.Load<TextAsset>("Skills");
        string itemsJson = itemText.text;
        JsonData j = JsonMapper.ToObject(itemsJson);
        foreach (JsonData temp in j)
        {
            SkillInfo info = new SkillInfo();
            info.id = int.Parse(temp["id"].ToString());
            info.name = temp["name"].ToString();
            info.mp = int.Parse(temp["mp"].ToString());
            info.coldTime = int.Parse(temp["coldTime"].ToString());
            info.applyTime = int.Parse(temp["applyTime"].ToString());
            info.distance = int.Parse(temp["distance"].ToString());
            info.level = int.Parse(temp["level"].ToString());
            info.skillType = (SkillType)System.Enum.Parse(typeof(SkillType), temp["skillType"].ToString());
            info.description = temp["description"].ToString();
            info.value = float.Parse(temp["mp"].ToString());
            info.sprite = temp["sprite"].ToString();
            info.effectPath = temp["effectPath"].ToString();
            skilllDict.Add(info.id, info);
        }
    }
}
