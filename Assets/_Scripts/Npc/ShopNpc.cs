using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNpc : MonoBehaviour
{
    private ShopPanel shopPanel;

    private void Start()
    {
        shopPanel = GameObject.Find("NpcPanel").transform.GetChild(0).GetComponent<ShopPanel>();
    }
    private void OnMouseOver()
    {
        CursorManager._instance.SeNpc();
        if (Input.GetMouseButtonDown(0))
        {
            shopPanel.Show();
        }
    }
    private void OnMouseExit()
    {
        CursorManager._instance.SetNormal();
    }
}
