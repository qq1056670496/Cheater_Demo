using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageCanvas : MonoBehaviour
{
    /// <summary>
    /// 文字预制体
    /// </summary>
    public GameObject hudText;
    private GameObject hud;

    private void Update()
    {
        Rotation();
    }

    /// <summary>
    /// 生成伤害文字
    /// </summary>
    public void ShowDamage(float damage)
    {
        hud = Instantiate(hudText, transform) as GameObject;
        hud.GetComponent<Text>().text =  damage.ToString();
    }
    public void ShowMiss()
    {
        hud = Instantiate(hudText, transform) as GameObject;
        hud.GetComponent<Text>().text ="Miss";
    }
    public void ShowCriticleDamage(float damage)
    {
        hud = Instantiate(hudText, transform) as GameObject;
        
        hud.GetComponent<Text>().text = "暴击  "+damage.ToString();
    }
    /// <summary>
    /// 画布始终朝向摄像机
    /// </summary>
    void Rotation()
    {
        this.transform.LookAt(Camera.main.transform);
    }
}
