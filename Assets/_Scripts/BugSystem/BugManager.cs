using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugManager : MonoBehaviour
{
    public static BugManager Instance;



    private float attachCdTimer=60;
    private AttachArea attachArea;

    private void Awake()
    {
        Instance = this;
        attachArea = GameObject.FindWithTag("Player").GetComponentInChildren<AttachArea>();
    }

    private void Update()
    {
        if(attachCdTimer<=10)
            attachCdTimer += Time.deltaTime;
    }

    public void AttachEnemys()
    {
        if (attachCdTimer >= 10)
        {
            attachArea.AttachEnemy();
            attachArea.AttachEnemy();
            attachCdTimer = 0;
        }
    }
}
