using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : BasePanel
{
    //public static SkillPanel _instance;
    private PlayerState ps;

    private Transform gridTrans;
    public GameObject skillItemPrefab;

    private SkillItem[] skillList;//存储SkillItem

    private Scrollbar skillScrollbar;

    public bool hasPickedSkill = false;

    private void Awake()
    {
        
        //ps = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        //gridTrans = transform.Find("ScrollArea/Grid");
        //skillScrollbar = transform.Find("Scrollbar").GetComponent<Scrollbar>();
    }
    void Start()
    {
        //foreach (int id in SkillManager._instance.skilllDict.Keys)
        //{
        //    skillItemPrefab.GetComponent<SkillItem>().SetId(id);
        //    Instantiate(skillItemPrefab, gridTrans);
        //}
        //skillList = transform.Find("ScrollArea/Grid").GetComponentsInChildren<SkillItem>();
        //UpdateSkillShow(ps.Level);
        
    }
    public override void Init()
    {
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        gridTrans = transform.Find("ScrollArea/Grid");
        skillScrollbar = transform.Find("Scrollbar").GetComponent<Scrollbar>();


        
    }
    public void StartInit()
    {
        foreach (int id in SkillManager._instance.skilllDict.Keys)
        {
            skillItemPrefab.GetComponent<SkillItem>().SetId(id);
            Instantiate(skillItemPrefab, gridTrans);
        }
        skillList = transform.Find("ScrollArea/Grid").GetComponentsInChildren<SkillItem>();
        UpdateSkillShow(ps.Level);
    }
    public void UpdateSkillShow(int level)
    {
        foreach (var skill in skillList)
        {
            skill.UpdateShow(level);
        }
    }
    public override void Show()
    {
        base.Show();
        skillScrollbar.value = 1;
    }
}
