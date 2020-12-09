using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ShortCutPanel:MonoBehaviour
{
    public static ShortCutPanel _instance;
    private ShortcutSlot[] slots;
    private void Awake()
    {
        _instance = this;
        slots = GetComponentsInChildren<ShortcutSlot>();
    }
    void Start()
    {
        transform.GetChild(0).GetComponent<ShortcutSlot>().SetSkill(1001);
        
    }



    public void Save()
    {
        //存
        foreach (var slot in slots)
        {
            slot.Save();
        }
    }
    public void Load()
    {
        foreach (var slot in slots)
        {
            slot.Load();
        }
    }
}
