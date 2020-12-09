using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    [SerializeField]private int level=1;
    private int exp = 0;
    private int totalExp = 50;


    private int hp;
    private int mp;
    public int basicMaxHp = 80;
    public int basicMaxMp = 70;

    private int remainPoints = 5;
    private int totalPoints = 5;

    private int money = 999;

    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            totalExp = level * 50;

            levelText.text = level.ToString();
            UpdateStates();

            UIPanelManager.Instance.SkillPanel.UpdateSkillShow(level);
        }
    }

    public int RemainPoints { get { return remainPoints; }
        set
        {
            remainPoints = value; UIPanelManager.Instance.PropertyPanel.UpdateRemainPoints();
        }
    }

    #region basicProperty
    [SerializeField]private int basicStrength = 10;
    private int basicIntellect = 10;
    private int basicAgility = 10;
    private int basicStamina = 10;
    [SerializeField]private float basicAttack = 10.0f;

    public int BasicStrength
    {
        get
        {
            return basicStrength;
        }
        set
        {
            basicStrength = value;
            UpdateStates();
        }
    }
    public int BasicIntellect
    {
        get
        {
            return basicIntellect;
        }
        set
        {
            basicIntellect = value; UpdateStates();
        }
    }
    public int BasicAgility
    {
        get
        {
            return basicAgility;
        }
        set
        {
            basicAgility = value; UpdateStates();
        }
    }
    public int BasicStamina
    {
        get
        {
            return basicStamina;
        }
        set
        {
            basicStamina = value; UpdateStates();
        }
    }
    public float BasicAttack
    {
        get
        {
            return basicAttack;
        }
        set
        {
            basicAttack = value;
        }
    }
    #endregion
    #region addProperty
    public int addStrength = 0;
    public int addIntellect = 0;
    public int addAgility = 0;
    public int addStamina = 0;
    public float addAttack = 0f;
    public int addHp = 0;
    public int addMp = 0;

    #endregion
    #region totalProperty
    public int totalStrength;
    public int totalIntellect;
    public int totalAgility;
    public int totalStamina;
    public float totalAttack;
    public int totalHp;
    public int totalMp;
    public float totalDefence = 0.05f;
    public float totalMissRate = 0.05f;
    public float totalCriticalRate = 0.05f;

    #endregion



    private bool isDead = false;
    [SerializeField] private bool isInvincible;
    private PlayerMove playerMove;
    public Slider hpSlider;
    public Slider mpSlider;
    public Slider expSlider;
    private Text expText;
    private Text hpText;
    private Text mpText;
    private GameObject startTip;
    public Text levelText;
    private DamageCanvas damageCanvas;

    public bool IsDead { get { return isDead; } }

    public int Money { get { return money; }
        set
        {
            money = value;
            //
            UIPanelManager.Instance.KnapsackPanel.UpdateMoneyText(money);
        }
    }

    private void Awake()
    {

        playerMove = GetComponent<PlayerMove>();
        hpText = hpSlider.GetComponentInChildren<Text>();
        mpText = mpSlider.GetComponentInChildren<Text>();
        expText = expSlider.GetComponentInChildren<Text>();
        damageCanvas = GetComponentInChildren<DamageCanvas>();
        startTip= GameObject.Find("StartTips");
    }
    private void Start()
    {
        UpdateStates();
        hp = totalHp;
        mp = totalMp;
        levelText.text = level.ToString();
        UpdateStates();
        UIPanelManager.Instance.PropertyPanel.UpdatePropertyPanel();

        UIPanelManager.Instance.KnapsackPanel.UpdateMoneyText(money);

        expSlider.value = (float)exp / totalExp;
        expText.text = "Exp:" + exp + "/" + totalExp;
    }
    private void Update()
    {

        //无敌
        if (playerMove.anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            isInvincible = true;
        }
        else
        {
            isInvincible = false;
        }
    }
    public void Damage(int damage)
    {
        if (IsDead) return;
        if (hp > 0 && !isInvincible)
        {
            if (Random.Range(0, 1f) < totalMissRate)//miss
            {
                damageCanvas.ShowMiss();
                return;
            }

            damageCanvas.ShowDamage(damage);
            SoundManager._instance.PlayAudio("FemaleDamage");
            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                isDead = true;
                playerMove.anim.SetTrigger("Dead");
                //复活返回城镇 deadTip点确定的时候调用Reborn
                UIPanelManager.Instance.DeadTip.SetActive(true);

            }
            else
            {
                //受伤特效
                StartCoroutine(DamageSetIsLock());
                playerMove.anim.SetBool("Walk", false);
                playerMove.anim.SetTrigger("Damage");
            }

            hpText.text = hp + "/" + totalHp;
            hpSlider.value = (float)hp / totalHp;
        }
    }
    IEnumerator DamageSetIsLock()
    {
        playerMove.isDamage = true;
        yield return new WaitForSeconds(0.566f);
        playerMove.isDamage = false;
    }
    public void UpdateStates()
    {
        totalStrength = basicStrength + addStrength;
        totalIntellect = basicIntellect + addIntellect;
        totalAgility = basicAgility + addAgility;
        totalStamina = basicStamina + addStamina;
        totalHp = basicMaxHp + addHp + totalStamina * 2;
        totalMp = basicMaxMp + addMp + totalIntellect * 3;
        totalAttack = basicAttack + addAttack + totalStrength;
        totalDefence = totalStamina * 0.0033f + 0.05f;
        totalMissRate = totalAgility * 0.0033f + 0.05f;

        UIPanelManager.Instance.PropertyPanel.UpdatePropertyPanel();
        UIPanelManager.Instance.PropertyPanel.UpdateRemainPoints();

        //生命魔力超出最大的时候变成最大
        if (hp > totalHp)
        {
            hp = totalHp;
        }
        if (mp > totalMp)
        {
            mp = totalMp;
        }
        UpdateHpAndMpSlider();
    }

    private void UpdateMp()
    {

        totalMp = basicMaxMp + addMp + totalIntellect * 3;
        if (mp > totalMp)
        {
            mp = totalMp;
        }
        UpdateHpAndMpSlider();
    }

    private void UpdateHpAndMpSlider()
    {
        hpText.text = hp + "/" + totalHp;
        mpText.text = mp + "/" + totalMp;
        hpSlider.value = (float)hp / totalHp;
        mpSlider.value = (float)mp / totalMp;
    }
    public void AddMp(int value)
    {
        mp += value;

        mpText.text = mp + "/" + totalMp;
        mpSlider.value = (float)mp / totalMp;
    }
    public void GetExp(int value)
    {
        exp += value;
        while (exp >= totalExp)
        {
            exp -= totalExp;
            RemainPoints += 5;

            Level++;
            basicStrength++;
            basicAgility++;
            basicIntellect++;
            basicStamina++;
            UpdateStates();//////
            hp = totalHp;
            mp = totalMp;
            UpdateHpAndMpSlider();
        }
        //经验条UI 
        expSlider.value = (float)exp / totalExp;
        expText.text = "Exp:" + exp + "/" + totalExp;

    }
    public void GetMedicine(int hp, int mp)
    {
        this.hp += hp;
        this.mp += mp;
        if (this.hp >= totalHp)
            this.hp = totalHp;
        if (this.mp >= totalMp)
            this.mp = totalMp;
        UpdateStates();
    }
    public bool TakeMp(int mp)
    {
        if (this.mp >= mp)
        {
            this.mp -= mp;

            UpdateMp();
            return true;
        }

        return false;
    }
    public bool UseMoney(int num)
    {
        if (Money - num >= 0)
        {
            Money -= num;
            return true;
        }
        return false;
    }
    public void Save()
    {
        PlayerPrefs.SetInt("basicAgility", basicAgility);
        PlayerPrefs.SetInt("basicStrength", basicStrength);
        PlayerPrefs.SetInt("basicIntellect", basicIntellect);
        PlayerPrefs.SetInt("basicStamina", basicStamina);
        PlayerPrefs.SetInt("basicMaxHp", basicMaxHp);
        PlayerPrefs.SetInt("basicMaxMp", basicMaxMp);

        PlayerPrefs.SetInt("addAgility", addAgility);
        PlayerPrefs.SetInt("addStrength", addStrength);
        PlayerPrefs.SetInt("addIntellect", addIntellect);
        PlayerPrefs.SetInt("addStamina", addStamina);
        PlayerPrefs.SetFloat("addAttack", addAttack);
        PlayerPrefs.SetInt("addHp", addHp);
        PlayerPrefs.SetInt("addMp", addMp);

        PlayerPrefs.SetInt("Level", Level);
        //PlayerPrefs.SetInt("hp", hp);
        //PlayerPrefs.SetInt("mp", mp);
        PlayerPrefs.SetInt("exp", exp);
        PlayerPrefs.SetInt("remainPoints", remainPoints);
        PlayerPrefs.SetInt("money", money);
        //PlayerPrefs.SetFloat("PosX", transform.position.x);
        //PlayerPrefs.SetFloat("PosY", transform.position.y);
        //PlayerPrefs.SetFloat("PosZ", transform.position.z);
    }
    public void Load()
    {
        basicAgility= PlayerPrefs.GetInt("basicAgility", basicAgility);
        basicStrength=PlayerPrefs.GetInt("basicStrength", basicStrength);
        basicIntellect = PlayerPrefs.GetInt("basicIntellect", basicIntellect);
        basicStamina = PlayerPrefs.GetInt("basicStamina", basicStamina);
        //
        basicMaxHp= PlayerPrefs.GetInt("basicMaxHp", basicMaxHp);
        basicMaxMp= PlayerPrefs.GetInt("basicMaxMp", basicMaxMp);

        addAgility = PlayerPrefs.GetInt("addAgility", addAgility);
        addStrength = PlayerPrefs.GetInt("addStrength", addStrength);
        addIntellect = PlayerPrefs.GetInt("addIntellect", addIntellect);
        addStamina = PlayerPrefs.GetInt("addStamina", addStamina);
        addAttack = PlayerPrefs.GetFloat("addAttack", addAttack);
        addHp = PlayerPrefs.GetInt("addHp", addHp);
        addMp = PlayerPrefs.GetInt("addMp", addMp);

        hp = totalHp;
        mp = totalMp;
        exp = PlayerPrefs.GetInt("exp", exp);
        remainPoints = PlayerPrefs.GetInt("remainPoints", remainPoints);
        money=PlayerPrefs.GetInt("money",999);
        //transform.position = new Vector3(
        //PlayerPrefs.GetFloat("PosX", transform.position.x),
        //PlayerPrefs.GetFloat("PosY", transform.position.y),
        //PlayerPrefs.GetFloat("PosZ", transform.position.z)
        //);
        UpdateStates();
        StartCoroutine(SetLevelDelay());
        startTip.SetActive(false);
        //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,8, Camera.main.transform.position.z);
    }
    IEnumerator SetLevelDelay()
    {
        yield return new WaitForSeconds(0.1f);

        Level = PlayerPrefs.GetInt("Level", Level);

        expSlider.value = (float)exp / totalExp;
        expText.text = "Exp:" + exp + "/" + totalExp;
    }
    public void ReBorn()
    {
        playerMove.anim.SetTrigger("ReBorn");
        transform.position = Vector3.zero;
        isDead = false;
        hp = totalHp;
        mp = totalMp;
        UpdateStates();
    }
}

