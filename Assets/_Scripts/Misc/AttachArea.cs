using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachArea : MonoBehaviour
{
    [SerializeField]private List<Transform> enemyTransList=new List<Transform>();
    private float attachTimer=0;
    private Transform attachPosTrans;
    private Vector3 tempPos;
    private SphereCollider _col;
    private void Awake()
    {
        attachPosTrans = transform.Find("AttachPos").transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyTransList.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy" )
        {
            enemyTransList.Remove(other.transform);
        }
    }

    private void Update()
    {
        
    }
    public void AttachEnemy()
    {
        tempPos = attachPosTrans.position;
        for (int i = enemyTransList.Count - 1; i >= 0; i--)
        {
            if (enemyTransList[i] == null)
            {
                enemyTransList.Remove(enemyTransList[i]);
                continue;
            }
            //enemyTransList[i].GetComponent<EnemyMove>().OnAttach();
            StartCoroutine(AttachEnemyDelay(enemyTransList[i], tempPos + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f))));

            //enemyTransList[i].position = tempPos;//+new Vector3(Random.Range(-0.5f,0.5f),0, Random.Range(-0.5f, 0.5f))
            //enemyTransList[i].GetComponent<EnemyMove>().OnAttach();
        }
    }
    IEnumerator AttachEnemyDelay(Transform trans,Vector3 pos)
    {
        float timer = 0;
        while (timer<0.5f)
        {
            timer += Time.deltaTime;
            trans.position = pos;
            yield return null;
        }
    }
}
