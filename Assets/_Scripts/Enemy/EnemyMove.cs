using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMove : MonoBehaviour
{
    public float walkRange=6;

    [HideInInspector] public Animator anim;
    protected Transform player;
    protected CharacterController cc;
    protected NavMeshAgent agent;
    protected EnemyState es;

    protected float patrolTimer = 0;
    protected float patrolTime = 2;


    private BoxCollider attackCol;
    public Vector3 bornPosition=Vector3.zero;

    public bool getDamage = false;

    protected bool isHit = false;

    [SerializeField]private bool isBackingToBorn;
    private bool inPatrol = false;


    [SerializeField]private Vector3 _destination;
    private bool _isStopped=false;
    [SerializeField] private float _speed=2.5f;
    private float _remainDistance
    {
        get
        {
            return Vector3.Distance(transform.position, _destination);
        }
    }
    

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        cc = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        es = GetComponent<EnemyState>();
        attackCol = transform.Find("AttackCol").GetComponent<BoxCollider>();
        patrolTime += Random.Range(0, 1f);
        
    }

    private void Start()
    {
        bornPosition = transform.position;
        //_destination = bornPosition;
        Patrol();
    }
    protected void Update()
    {
        if (es.IsDead) return;
        

        if (_remainDistance < 0.1f)
        {
            _isStopped = true; 
        }
        if (_isStopped == false&& _remainDistance>0.3f)
        {
            transform.LookAt(_destination);
            cc.SimpleMove(transform.forward * _speed);
            //transform.Translate(transform.forward * _speed * Time.deltaTime);
        }
        if (isBackingToBorn)
        {
            if (_remainDistance < 0.1f)
            {
                isBackingToBorn = false;
                _isStopped = true;
            }
            //else if (_isStopped == false)
            //{
            //    return;
            //}
        }




        //超出4倍
        if (Vector3.Distance(transform.position, bornPosition) > 4*walkRange|| (_remainDistance > walkRange&& getDamage==false))
        {
            isBackingToBorn = true;
            _destination = bornPosition;

            anim.SetBool("Walk", true);
            _isStopped = false;
            es.hp = es.maxHp;
            es.UpdateSlider();
            getDamage = false;
            return;
        } 
        //小于侦察范围   //大于侦察范围 ，被攻击也要追击 追击距离2*walkRange  
        else if (Vector3.Distance(player.position, transform.position) < walkRange || (Vector3.Distance(player.position, transform.position) < 2 * walkRange && getDamage))
        {
            patrolTimer = 0;
            //大于攻击距离追击
            if (Vector3.Distance(player.position, transform.position) > es.attackRange)
            {
                anim.SetBool("Walk", true);
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    _isStopped = false;
                    _destination = player.position;
                }
            }
            else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))//进入攻击距离
            {
                anim.SetTrigger("Attack");
                _isStopped = true;
                transform.LookAt(player);
            }
        }
        else if (Vector3.Distance(transform.position, bornPosition) >walkRange)
        {
            isBackingToBorn = true;
            _destination = bornPosition;

            anim.SetBool("Walk", true);
            _isStopped = false;
            es.hp = es.maxHp;
            es.UpdateSlider();
        }
        else
        {
            getDamage = false;
            patrolTimer += Time.deltaTime;//2s 站立和巡逻切换
            if (inPatrol &&_remainDistance<=0.3f)//时间没到但是已经到了位置
            {
                patrolTimer = patrolTime;
                //_isStopped = true;
            }
            if (patrolTimer >= patrolTime)
            {
                inPatrol = false;
                patrolTimer = 0;
                if (_isStopped)
                    Patrol();
                else
                {
                    Stand();
                }
            }
        }
    }

    public void StartAttackSetCol()
    {
        StartCoroutine(AttackSetCol());
    }
    IEnumerator AttackSetCol()
    {
        attackCol.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        attackCol.gameObject.SetActive(false);
    }

    protected void Stand()
    {
        anim.SetBool("Walk", false);
        _isStopped = true;
    }
    protected void Patrol()//
    {
        inPatrol = true;
        _isStopped = false;
        anim.SetBool("Walk", true);
        
        while (_remainDistance < 3f)
        {
            _destination = bornPosition + new Vector3(Random.Range(0f, walkRange), 0, Random.Range(0f, walkRange));
        }
    }


    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerWeapon")
        {
            if (isHit) return;
            StartCoroutine(DamageTimer());
            transform.SendMessage("Damage", player.GetComponent<PlayerState>().totalAttack);
        }
    }
    protected IEnumerator DamageTimer()
    {
        isHit = true;
        yield return new WaitForSeconds(0.2f);
        isHit = false;
    }
    public void OnAttach()
    {
        _destination = transform.position;
    }

   
}
