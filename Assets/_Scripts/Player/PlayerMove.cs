using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerMove : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    private PlayerState playerState;
    //public NavMeshAgent agent;
    private Rigidbody _rigid;
    private CharacterController _cc;
    
    [SerializeField]private Camera _mainCam;


    [SerializeField]private bool _canMove = true;
    public bool CanMove
    {
        get
        {
            return _canMove;
        }
        set
        {
            _canMove = value;
        }
    }
    public float _moveSpeed = 4f;
    public float jumpForce = 1;
    public bool isDamage;
    public bool isAttack=false;

    public bool canUseSkil=true;

    public BoxCollider weaponCol;
    private float idleChangeTimer = 0;


    //记录当前翻滚方向 
    private Vector3 _rollTargetPos;

    //移动
    private float _moveX;//水平输入
    private float _moveZ;
    private Vector3 _direction;

    [SerializeField]private bool isGround;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
        //agent = GetComponent<NavMeshAgent>();
        _rigid = GetComponent<Rigidbody>();
        _cc = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (playerState.IsDead) return;

        

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle0"))//|| anim.GetCurrentAnimatorStateInfo(0).IsName("Walk")
            isAttack = false;
        else //if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            //agent.isStopped = true;
            _canMove = true;
        }
        //else
        //{
        //    //agent.isStopped = false;
        //    //_canMove = true;
        //}

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            canUseSkil = false;

        }
        else
        {
            canUseSkil = true;
        }

        Idle();
        Roll();
        
    }

    private void FixedUpdate()
    {
        if (playerState.IsDead) return;
        //Jump();
        Walk();
    }
    private void Idle()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle0")||!isAttack)
        {
            idleChangeTimer += Time.deltaTime;
            if (idleChangeTimer >= 5)
            {
                anim.SetTrigger("IdleChange");
                idleChangeTimer = 0;
            }
        }
    }


    //private void Walk()
    //{

    //    if (agent.remainingDistance <= 0.1f)
    //    {
    //        anim.SetBool("Walk", false);
    //        agent.isStopped = true;
    //    }
        
    //    if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(0))//没有在UI上，
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hitInfo;
    //        int layermask = ~((1<<9)|(1<<12)|(1<<2));
    //        bool isCollider = Physics.Raycast(ray, out hitInfo,99,layermask);//
    //        //Debug.Log(hitInfo.transform.gameObject);
    //        if (isCollider&&!anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
    //        {
    //            if( !isAttack)
    //                transform.LookAt(new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z));
    //            if(hitInfo.transform.tag == "Ground")
    //            {
    //                targetPos = hitInfo.point;
    //                if (isDamage|| isAttack
    //                || InventoryManager.Instance.pickedItem.gameObject.activeInHierarchy
    //                || UIPanelManager.Instance.SkillPanel.hasPickedSkill)
    //                    return;

    //                if (hitInfo.point.y < -0.2) return;
    //                anim.SetBool("Walk", true);
    //                agent.SetDestination(hitInfo.point);
    //                agent.isStopped = false;
    //            }
    //        }
    //    }
    //}
    private void Walk()
    {
        _moveX = Input.GetAxisRaw("Horizontal");
        _moveZ = Input.GetAxisRaw("Vertical");
        
        //在攻击和翻滚的时候返回
        if (!_canMove||anim.GetCurrentAnimatorStateInfo(0).IsName("Roll") || isAttack)//|| anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1")
            //|| anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2")
            //|| anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3")
            return;

        if(_moveX!=0 || _moveZ != 0)
        {

            ////获取方向
            //_direction = new Vector3(_moveX, 0, _moveZ);
            ////将方向转换为四元数
            //Quaternion quaDir = Quaternion.LookRotation(_direction, Vector3.up);
            ////缓慢转动到目标点
            //transform.rotation = Quaternion.Lerp(transform.rotation, quaDir, Time.fixedDeltaTime * 60);


            _direction = new Vector3(_moveX, 0, _moveZ);
            transform.LookAt(Vector3.Lerp(transform.position, transform.position+_direction,Time.deltaTime*60));
            transform.eulerAngles += _mainCam.transform.eulerAngles.y * Vector3.up;
            //_cc.SimpleMove(transform.forward * _moveSpeed);
            _rigid.velocity = transform.forward*_moveSpeed + Vector3.up*_rigid.velocity.y;
            anim.SetBool("Walk", true);
        }
        else
        {
            _rigid.velocity= Vector3.up * _rigid.velocity.y;
            anim.SetBool("Walk", false);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void Dead()
    {
        anim.SetTrigger("Dead");
    }

    private void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)&&!anim.GetCurrentAnimatorStateInfo(0).IsName("Damage")&&!anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            Vector3 tempDir = transform.position + _mainCam.transform.forward;
            transform.LookAt( new Vector3(tempDir.x,transform.position.y,tempDir.z));
            //transform.LookAt(transform.position + transform.forward*_moveZ+transform.right*_moveX);
            _canMove = false;
            _rigid.velocity = _rigid.velocity.y * Vector3.up;
            anim.SetTrigger("Roll");
            StartCoroutine(AfterRollDelay());

            
            //rigid.AddForce(transform.forward * force,ForceMode.VelocityChange);
        }
    }
    IEnumerator AfterRollDelay()
    {
        yield return new WaitForSeconds(0.2f);
        _rigid.AddForce(transform.forward * 40000);
        yield return new WaitForSeconds(0.5f);
        if (_moveX == 0 && _moveZ == 0)
        {
            anim.SetBool("Walk", false);
        }
        else
        {
            anim.SetBool("Walk", true);
        }
    }


    public void SwitchWeaponCol(int i)
    {
        if (i == 1)
        {
            weaponCol.enabled = true;
        }
        else
        {
            weaponCol.enabled = false;
        }
    }
    private void SetWeaponRange(int range)
    {
        weaponCol.size =new Vector3(weaponCol.size.x,weaponCol.size.y, range);
    }

}
