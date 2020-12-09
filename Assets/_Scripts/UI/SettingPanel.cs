using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingPanel : BasePanel
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
        _slider.onValueChanged.AddListener((float value)=>SoundManager._instance.ChangeSoundVolume(value));
    }

    public override void Init()
    {
        
    }



}
