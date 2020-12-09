using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerState ps;
    private PlayerMove pm;
    private BoxCollider normalAttackCol;
    private Animator anim;
    private Rigidbody _rigid;
    public bool isLockedDirection = false;
    public bool isLockedPosition= false;

    public bool isUsingSkill;

    private Transform effectTrans;
    private GameObject circleEffect;

    private SkillInfo currentInfo;
    private ShortcutSlot currentSlot;

    private GameObject _blueCircle;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        effectTrans = GameObject.Find("Effects").transform;
        normalAttackCol = transform.Find("NormalAttackCol").GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        _blueCircle = transform.Find("BlueCircle").gameObject;
    }

    private void Update()
    {
        if (!pm.canUseSkil) return;
        //方向技能释放
        if (isLockedDirection)
        {
            isUsingSkill = true;
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(DirectionLocked());
                SoundManager._instance.PlayAudio("FemaleSpell");
                CursorManager._instance.SetNormal();
                isLockedDirection = false;

                isUsingSkill = false;
                StartCoroutine(SetIsAttackDelay());
            }
            else if(Input.GetMouseButtonDown(1))
            {
                //返cd
                currentSlot.coldTimer = 0;
                //返蓝 
                ps.AddMp(currentInfo.mp);
                CursorManager._instance.SetNormal();
                isLockedDirection = false;
                isUsingSkill = false;
            }
        }
        //范围技能释放
        if (isLockedPosition)
        {
            isUsingSkill = true;
            if (circleEffect != null && circleEffect.activeInHierarchy)//让圈跟着鼠标移动  
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                bool isCollider = Physics.Raycast(ray, out hitInfo, 99,1 << LayerMask.NameToLayer("Ground"));//
                if (isCollider&&hitInfo.transform.tag=="Ground")
                {
                    if(Vector3.Distance(transform.position, hitInfo.point)<=currentInfo.distance)//限制范围 
                    {
                        circleEffect.transform.position = hitInfo.point+Vector3.up*0.1f;
                        if (Input.GetMouseButtonDown(0))
                        {
                            StartCoroutine(PositionLocked());
                            CursorManager._instance.SetNormal();
                            isLockedPosition = false;
                            ObjectPoolManager._instance.UnSpawn(circleEffect.gameObject);
                            isUsingSkill = false;
                            SoundManager._instance.PlayAudio("FemaleSpell");
                            StartCoroutine(SetIsAttackDelay());

                            _blueCircle.SetActive(false);
                        }
                        else if (Input.GetMouseButtonDown(1))
                        {
                            //返cd
                            currentSlot.coldTimer = 0;
                            //返蓝 
                            ps.AddMp(currentInfo.mp);
                            CursorManager._instance.SetNormal();
                            isLockedPosition = false;
                            ObjectPoolManager._instance.UnSpawn(circleEffect.gameObject);
                            isUsingSkill = false;

                            _blueCircle.SetActive(false);
                        }
                    }

                }
            }
            
        }
    }

    public void UseSkill(SkillInfo info,ShortcutSlot slot)
    {
        if (ps.IsDead||isUsingSkill||!pm.canUseSkil) return;
        _rigid.velocity = Vector3.up * _rigid.velocity.y;
        currentInfo = info;
        currentSlot = slot;
        switch (info.skillType)
        {
            case SkillType.Buff:
                UseBuff(info);
                StartCoroutine(SetIsAttackDelay());
                break;
            case SkillType.Normal:
                UseNormal(info);
                StartCoroutine(SetIsAttackDelay());
                break;
            case SkillType.Direction:
                UseDirection(info);
                break;
            case SkillType.Position:
                UsePosition(info);
                break;
            default:
                break;
        }
        
    }
    IEnumerator SetIsAttackDelay()
    {
        pm.isAttack = true;
        yield return new WaitForSeconds(0.1f);
        pm.isAttack = true;
        anim.SetBool("Walk", false);
    }



    private void UseBuff(SkillInfo info)
    {
        //动作 特效 持续  
        anim.SetTrigger("Buff");
        switch (info.name)
        {
            case "战吼":
                ObjectPoolManager._instance.Spawn("Growl_Effect", transform);
                StartCoroutine(BuffGrowlTimer(info.applyTime));
                SoundManager._instance.PlayAudio("FemaleGrowl");
                break;
            default:
                break;
        }
    }
    IEnumerator BuffGrowlTimer(int applyTime)
    {
        ps.totalAttack *= 1.2f;

        UIPanelManager.Instance.PropertyPanel.UpdatePropertyPanel();
        yield return new WaitForSeconds(applyTime);
        ps.UpdateStates();
        UIPanelManager.Instance.PropertyPanel.UpdatePropertyPanel();
    }

    private void UseNormal(SkillInfo info)
    {
        anim.SetTrigger("Attack");
    }


    //在动画里设置碰撞和声音开始计时
    public void StartSetNormalAttackCol()
    {
        StartCoroutine(SetNormalAttackCol());
    }

    IEnumerator SetNormalAttackCol()
    {
        SoundManager._instance.PlayAudio("FemaleAttack" + Random.Range(1, 3));
        normalAttackCol.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.22f);
        normalAttackCol.gameObject.SetActive(false);
    }
    private void UseDirection(SkillInfo info)
    {
        //1.鼠标变成 Direction Cursor 
        //2.启用coroutine
        CursorManager._instance.SetDirection();
        isLockedDirection = true;
    }
    IEnumerator DirectionLocked()
    {
        //检测地面碰撞让主角面向方向 动作  特效产生 伤害
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        bool isCollider = Physics.Raycast(ray, out hitInfo, 99, 1 << LayerMask.NameToLayer("Ground"));
        if (isCollider)
        {
            pm.isAttack = true;
            anim.SetTrigger("DirectionSpell");
            StartCoroutine(SetIsAttackDelay());
            transform.LookAt(new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z));
            yield return new WaitForSeconds(0.55f);
            GameObject go = ObjectPoolManager._instance.Spawn("FireBird_Effect", effectTrans);
            go.transform.position = transform.position;
            go.transform.eulerAngles = transform.eulerAngles;
        }
    }
    private void UsePosition(SkillInfo info)
    {
        CursorManager._instance.SetPosition();
        isLockedPosition = true;
        circleEffect = ObjectPoolManager._instance.Spawn("CircleQuad", effectTrans);
        _blueCircle.SetActive(true);
    }
    IEnumerator PositionLocked()
    {
        //检测地面碰撞让主角面向方向 动作  特效产生 伤害
        
        transform.LookAt(new Vector3(circleEffect.transform.position.x, transform.position.y, circleEffect.transform.position.z));
        anim.SetTrigger("PositionSpell");
        StartCoroutine(SetIsAttackDelay());
        yield return new WaitForSeconds(0.5f);
        GameObject go = ObjectPoolManager._instance.Spawn("BlackHits_Effect", effectTrans);
        go.transform.position = circleEffect.transform.position+Vector3.up*1f;
        go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }


}
