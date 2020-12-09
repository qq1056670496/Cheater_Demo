using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolTip : MonoBehaviour
{

    public static ToolTip _instance;

    private Text toolTipText;
    private Text contentText;
    private void Awake()
    {
        _instance = this;
        toolTipText = GetComponent<Text>();
        contentText = transform.Find("ContentText").GetComponent<Text>();
    }
    public void Show(string text)
    {
        toolTipText.gameObject.SetActive(true);
        toolTipText.text = text;
        contentText.text = text;
    }
    public void Hide()
    {
        toolTipText.gameObject.SetActive(false);
    }
    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
}
