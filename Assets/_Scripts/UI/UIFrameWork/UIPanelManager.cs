using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    private static UIPanelManager _instance;

    public static UIPanelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Canvas").GetComponent<UIPanelManager>();
            }
            return _instance;
        }
    }


    public CharacterPanel CharacterPanel;
    public Knapsack KnapsackPanel;

    public SkillPanel SkillPanel;
    public ShopPanel ShopPanel;
    public QuestPanel QuestPanel;
    public SettingPanel SettingPanel;
    public PropertyPanel PropertyPanel;

    public GameObject DeadTip;

    private void Awake()
    {
        CharacterPanel.Init();
        KnapsackPanel.Init();
        SkillPanel.Init();
        
        QuestPanel.Init();
        SettingPanel.Init();
        PropertyPanel.Init();

        ShopPanel.Init();
    }
    private void Start()
    {
        ShopPanel.StartInit();
        SkillPanel.StartInit();
    }


}
