using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public void ExitBtn()
    {
        GameManager._instance.ExitGameAndSave();
    }
    public void MenuBtn()
    {
        GameManager._instance.BackToMenu();
    }
    public void ClickSound()
    {
        SoundManager._instance.OnButtonClick();
    }
}
