using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestNpc : MonoBehaviour
{
    public static QuestNpc _instance;
    
    

    

    private void Start()
    {
        _instance = this;

    }
    private void OnMouseOver()
    {
        CursorManager._instance.SeNpc();
        if (Input.GetMouseButtonDown(0))
        {
            QuestManager.Instance.QuestPanel.ShowOrHide();
            if (QuestManager.Instance.IsInQuest)
            {
                QuestManager.Instance.SmallHpMedicineNum = UIPanelManager.Instance.KnapsackPanel.GetNumById(1);
                if (QuestManager.Instance.SmallHpMedicineNum >= 5 && QuestManager.Instance.KillSkeletonNum >= 10)
                {
                    ShowQuestEnd();
                }
                else
                {
                    ShowQuestProgress();
                }
            }
            else
            {
                ShowQuestTarget();
            }
        }
    }
    private void OnMouseExit()
    {
        CursorManager._instance.SetNormal();
    }



    private void ShowQuestTarget()
    {
        QuestManager.Instance.QuestText.text = "草原出现了很多<color=red>骷髅怪</color>,请你去消灭10只<color=red>骷髅怪</color>并带回5瓶<color=red>小瓶血药</color>,我会给你不错的奖励.\n对了!在前方的绿色传送点可以传送到草原.";
        QuestManager.Instance.SubmitBtn.gameObject.SetActive(false);
        QuestManager.Instance.AccetpBtn.gameObject.SetActive(true);
    }
    private void ShowQuestProgress()
    {
        QuestManager.Instance.QuestText.text = "你已经消灭:"+ QuestManager.Instance.KillSkeletonNum + "/10只<color=red>骷髅怪</color>\n你已经拥有:"+ QuestManager.Instance.SmallHpMedicineNum +"/5瓶<color=red>小瓶血药</color>\n继续努力吧!";
        QuestManager.Instance.SubmitBtn.gameObject.SetActive(false);
        QuestManager.Instance.AccetpBtn.gameObject.SetActive(false);
    }
    private void ShowQuestEnd()
    {
        QuestManager.Instance.QuestText.text = "你杀死了骷髅并带回了血药\n领取你的奖励吧!";
        QuestManager.Instance.SubmitBtn.gameObject.SetActive(true);
        QuestManager.Instance.AccetpBtn.gameObject.SetActive(false);
    }
    //public void KillSkeleton()
    //{
    //    if(QuestManager.Instance.IsInQuest)
    //        QuestManager.Instance.KillSkeletonNum++;
    //}

}
