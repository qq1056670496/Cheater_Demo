using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyState : MonoBehaviour
{


    public int maxHp;
    public int hp;
    private GameObject sack;
    private Transform dropItemTrans;

    public int attack;
    public float attackRange=2;
    public string DamageSound;
    private bool isDead;
    private EnemyMove em;
    private DamageCanvas damageCanvas;
    private Slider hpSlider;
    private PlayerState ps;
    public int level = 1;

    public bool IsDead { get { return isDead; }set { isDead = value; } }

    private void Awake()
    {
        hp = maxHp;
        em = GetComponent<EnemyMove>();
        damageCanvas = transform.GetComponentInChildren<DamageCanvas>();
        hpSlider = damageCanvas.GetComponentInChildren<Slider>();
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        transform.GetComponentInChildren<Text>().text="LV."+level;
        dropItemTrans = GameObject.Find("DropItems").transform;
    }
    public void Damage(int damage)
    {
        if (isDead) return;
        //miss
        if (Random.Range(0, 1f) < 0.05f)
        {
            UpdateSlider();

            if (damage == 0) return;
            damageCanvas.ShowMiss();
            return;
        }

        hp -=damage;

        UpdateSlider();
        if (damage == 0) return;
        //伤害显示
        
        damageCanvas.ShowDamage(damage);
        PlayDamageSound();
        
        if (hp <= 0)
        {
            hp = 0;

            ps.GetExp(level * 20);
            //播放死亡
            em.anim.SetTrigger("Dead");
            //资源池// 销毁
            OnDead();
            isDead = true;
        }
        else 
        {
            //播放受伤
            em.anim.SetTrigger("Damage");
            em.getDamage = true;
        }
    }
    public virtual void OnDead()
    {
        GetComponent<CharacterController>().enabled = false;
        StartCoroutine(OnDeadDelay());
    }
    IEnumerator OnDeadDelay()
    {
        Vector3 tempPos = transform.position;
        //掉落金钱
        GameObject gold = ObjectPoolManager._instance.Spawn("Gold", dropItemTrans);
        gold.GetComponent<GoldObject>().SetGoldNum(level);
        //掉落物品
        while (Random.Range(0, 1.0f) < 0.5)
        {
            sack = ObjectPoolManager._instance.Spawn("Sack", dropItemTrans);
            sack.GetComponent<SackObject>().SetItemId();
            sack.transform.position = tempPos + new Vector3(Random.Range(-2, 2f), 1f, Random.Range(-2, 2f));
        }

        //设置位置
        gold.transform.position = tempPos + new Vector3(Random.Range(-2, 2f), 1f, Random.Range(-2, 2f));

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        //ObjectPoolManager._instance.UnSpawn(gameObject);
    }
    public void UpdateSlider()
    {
        hpSlider.value = (float)hp / maxHp;
    }
    private void PlayDamageSound()
    {
        StartCoroutine(PlayDamageSoundDelay());
    }
    IEnumerator PlayDamageSoundDelay()
    {
        yield return new WaitForSeconds(0.1f);
        SoundManager._instance.PlayAudio(DamageSound);
    }
}
