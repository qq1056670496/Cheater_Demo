using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public Button AccetpBtn;
    public Button CancelBtn;
    public Button SubmitBtn;

    public Text QuestText;

    public int KillSkeletonNum = 0;
    public int SmallHpMedicineNum = 0;
    public bool IsInQuest = false;
    private PlayerState ps;
    public QuestPanel QuestPanel;
    private void Awake()
    {
        Instance = this;
        QuestPanel = GameObject.Find("NpcPanel").transform.GetChild(1).GetComponent<QuestPanel>();
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
    }
    // Start is called before the first frame update
    void Start()
    {
        AccetpBtn = QuestPanel.transform.Find("AcceptBtn").GetComponent<Button>();
        CancelBtn = QuestPanel.transform.Find("CancelBtn").GetComponent<Button>();
        SubmitBtn = QuestPanel.transform.Find("SubmitBtn").GetComponent<Button>();
        QuestText = QuestPanel.transform.Find("QuestInfo").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCancelBtnClick()
    {
        QuestPanel.ShowOrHide();
    }
    public void OnAcceptBtnClick()
    {
        QuestPanel.ShowOrHide();
        QuestManager.Instance.IsInQuest = true;
    }
    public void OnSubmitBtnClick()
    {
        QuestPanel.ShowOrHide();
        QuestManager.Instance.IsInQuest = false;
        QuestManager.Instance.KillSkeletonNum = 0;
        //药水变化
        UIPanelManager.Instance.KnapsackPanel.ReduceById(1, 5);
        ps.GetExp(200);
        ps.Money += 1000;
    }
}
