using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackCol : MonoBehaviour
{
    private List<GameObject> hittedGoList = new List<GameObject>();
    private PlayerState ps;
    private void Awake()
    {
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            int index = hittedGoList.IndexOf(other.gameObject);
            if (index == -1)
            {
                other.GetComponent<EnemyState>().Damage((int)(ps.totalAttack));
                hittedGoList.Add(other.gameObject);
            }
        }
    }
    private void OnDisable()
    {
        hittedGoList.Clear();
    }
}
