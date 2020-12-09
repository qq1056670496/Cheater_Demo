using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBirdObject : Effect
{
    private List<GameObject> hittedGoList = new List<GameObject>();
    private PlayerState ps;
    private BoxCollider _boxCol;
    private void Awake()
    {
        _boxCol = GetComponent<BoxCollider>();
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
        StartCoroutine(BoxCor());
    }
    IEnumerator BoxCor()
    {
        yield return new WaitForSeconds(0.5f);
        _boxCol.enabled = false;
    }
    public override void OnUnSpawn()
    {
        _boxCol.enabled = true;
        hittedGoList.Clear();
        base.OnUnSpawn();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            int index = hittedGoList.IndexOf(other.gameObject);
            if (index == -1)
            {
                other.GetComponent<EnemyState>().Damage((int)(ps.totalAttack * 1.5f));
                hittedGoList.Add(other.gameObject);
            }
        }
    }
    //private void Update()
    //{
    //    transform.Translate(transform.forward * Time.deltaTime * 10, Space.World);
    //}
}
