using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningTip : MonoBehaviour
{
    public static WarningTip _instance;
    private Text warnText;
    private void Awake()
    {
        _instance = this;
        warnText = GetComponentInChildren<Text>();
        Hide();
    }

    public void Show(string tip)
    {
        warnText.text = tip;
        gameObject.SetActive(true);
        StartCoroutine(HideDelay());
    }

    IEnumerator HideDelay()
    {
        yield return new WaitForSeconds(3f);
        Hide();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
