using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : BasePanel
{
    private PlayerMove pm;
    // Start is called before the first frame update
    void Start()
    {
        //pm = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
    }
    public override void Init()
    {

        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
    }

    public override void Show()
    {
        pm.CanMove = false;
        pm.anim.SetBool("Walk", false);
        base.Show();

    }
}
