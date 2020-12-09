using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCol : MonoBehaviour
{
    private bool hasAttacked = false;
    private EnemyState es;
    private PlayerState ps;
    private void Awake()
    {
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        es = GetComponentInParent<EnemyState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&&!hasAttacked)
        {
            //造成伤害
            ps.Damage(es.attack);
            hasAttacked = true;
        }
    }
    private void OnDisable()
    {
        hasAttacked = false;
    }
}
