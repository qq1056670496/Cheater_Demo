using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHitsObject : Effect
{
    private List<GameObject> hittedGoList = new List<GameObject>();
    private PlayerState ps;
    private void Awake()
    {
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
    }
    public override void OnUnSpawn()
    {
        hittedGoList.Clear();
        base.OnUnSpawn();
    }
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            for (int i = 0; i < 8; i++)
            {
                HitOnce(other.gameObject);
                yield return new WaitForSeconds(0.2f);

                hittedGoList.Clear();
            }
        }
    }
    private void HitOnce(GameObject go)
    {
        int index = hittedGoList.IndexOf(go.gameObject);
        if (index == -1)
        {
            go.GetComponent<EnemyState>().Damage((int)(ps.totalAttack * 0.5f));
            hittedGoList.Add(go);
        }
    }
}
