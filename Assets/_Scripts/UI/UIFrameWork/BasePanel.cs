using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    public bool IsShow = false;

    public abstract void Init();
    public virtual void Hide()
    {
        gameObject.SetActive(false);
        IsShow = false;
    }
    public virtual void Show()
    {
        gameObject.SetActive(true);
        IsShow = true;
    }
    public virtual void ShowOrHide()
    {
        if (IsShow)
        {
            Hide();
            
        }
        else
        {
            Show();
        }

    }
    public virtual void OnEnter()
    {
        
    }
    public virtual void OnExit()
    {

    }
}
