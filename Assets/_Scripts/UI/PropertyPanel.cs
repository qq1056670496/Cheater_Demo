using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyPanel : BasePanel
{

    private PlayerState ps;

    private Text strengthText;
    private Text intellectText;
    private Text agilityText;
    private Text staminaText;
    private Text leftPropertyText;
    private Text rightPropertyText;
    private Text remainPointsText;

    public Button strengthBtn;
    public Button intellectBtn;
    public Button agilityBtn;
    public Button staminaBtn;

    private void Awake()
    {
        //ps = GameObject.FindWithTag("Player").GetComponent<PlayerState>();

        //strengthText = transform.Find("StrengthText").GetComponent<Text>();
        //intellectText = transform.Find("IntellectText").GetComponent<Text>();
        //agilityText = transform.Find("AgilityText").GetComponent<Text>();
        //staminaText = transform.Find("StaminaText").GetComponent<Text>();
        //leftPropertyText = transform.Find("LeftPropertyText").GetComponent<Text>();
        //rightPropertyText = transform.Find("RightPropertyText").GetComponent<Text>();
        //remainPointsText = transform.Find("RemainPoints").GetComponent<Text>();
        //UpdateRemainPoints();
    }

    public override void Init()
    {
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerState>();

        strengthText = transform.Find("StrengthText").GetComponent<Text>();
        intellectText = transform.Find("IntellectText").GetComponent<Text>();
        agilityText = transform.Find("AgilityText").GetComponent<Text>();
        staminaText = transform.Find("StaminaText").GetComponent<Text>();
        leftPropertyText = transform.Find("LeftPropertyText").GetComponent<Text>();
        rightPropertyText = transform.Find("RightPropertyText").GetComponent<Text>();
        remainPointsText = transform.Find("RemainPoints").GetComponent<Text>();
        UpdateRemainPoints();

    }

    public void UpdateRemainPoints()
    {
        remainPointsText.text = "剩余属性点数：" + ps.RemainPoints.ToString();
        UpdateAddBtn();
    }

    public void UpdatePropertyPanel()
    {
        
        strengthText.text = "力量：" + (ps.totalStrength).ToString();
        intellectText.text = "智慧：" + (ps.totalIntellect).ToString();
        agilityText.text = "敏捷：" + (ps.totalAgility).ToString();
        staminaText.text = "体力：" + (ps.totalStamina).ToString();

        leftPropertyText.text = string.Format("HP：{0}\n攻击力：{1}\n伤害抵挡：{2}%",
            ps.totalHp,
            ps.totalAttack.ToString("f1"),
            (ps.totalDefence * 100).ToString("f1")
            ) ;
        rightPropertyText.text = string.Format("MP：{0}\n暴击率：{1}%\n闪避几率：{2}%",ps.totalMp,5,(ps.totalMissRate*100).ToString("f1"));
    }
    //更新是否能加点
    public void UpdateAddBtn()
    {
        if(ps.RemainPoints>0)
        {
            strengthBtn.interactable = true;
            intellectBtn.interactable = true;
            agilityBtn.interactable = true;
            staminaBtn.interactable = true;
        }
        else
        {
            strengthBtn.interactable = false;
            intellectBtn.interactable = false;
            agilityBtn.interactable = false;
            staminaBtn.interactable = false;
        }
    }
    #region 按钮事件
    public void AddStrengthBtn()
    {
        if (ps.RemainPoints > 0)
        {
            ps.RemainPoints--;
            ps.BasicStrength += 1;
        }
        UpdatePropertyPanel();
        UpdateAddBtn();
    }
    public void AddIntellectBtn()
    {
        if (ps.RemainPoints > 0)
        {
            ps.RemainPoints--;
            ps.BasicIntellect += 1;
        }
        UpdatePropertyPanel();
        UpdateAddBtn();
    }
    public void AddAgilityBtn()
    {
        if (ps.RemainPoints > 0)
        {
            ps.RemainPoints--;
            ps.BasicAgility += 1;
        }
        UpdatePropertyPanel();
        UpdateAddBtn();
    }
    public void AddStaminaBtn()
    {
        if (ps.RemainPoints > 0)
        {
            ps.RemainPoints--;
            ps.BasicStamina += 1;
        }
        UpdatePropertyPanel();
        UpdateAddBtn();
    }
    #endregion
}
