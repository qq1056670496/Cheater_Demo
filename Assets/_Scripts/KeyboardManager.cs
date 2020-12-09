using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    Knapsack.Instance.StoreItem(Random.Range(1, 9));
        //}
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIPanelManager.Instance.KnapsackPanel.ShowOrHide();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            UIPanelManager.Instance.CharacterPanel.ShowOrHide();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            UIPanelManager.Instance.SkillPanel.ShowOrHide();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIPanelManager.Instance.SettingPanel.ShowOrHide();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BugManager.Instance.AttachEnemys();
        }
    }
}
