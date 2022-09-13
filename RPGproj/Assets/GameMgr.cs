using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    // ���ӸŴ��� �̱��� //
    private GameMgr() { }
    private static GameMgr instance = null;
    public static GameMgr GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("������Ʈ�� ã�� �� �����ϴ�.");
        }
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }

    // �÷��̾� ���°�
    int Dmg;                // �⺻ ������
    int AttackDamage;       // ���� ������

    int Def;                // �⺻ ����
    int Defense;            // ���� ����

    [SerializeField]
    TextMeshProUGUI Text_Attack;
    [SerializeField]
    TextMeshProUGUI Text_Defense;


    float Speed = 1.0f;     // �̵��ӵ�
    float Jump = 400.0f;    // ������
    int isJump = 0;         // 0 = ���� ������ ���� 1 = ������(���) 2 = ������(�ϰ�)
    bool IsGround;          // �ٴڰ� ����ִ��� üũ

    // ��� �߰��ɷ�ġ
    int WeaponDmg;
    int ArmorDef;

    int MaxHP;
    int HP;

    int MaxMP;
    int MP;

    int Level;
    int MaxEXP;
    int EXP;
    [SerializeField]
    Slider ExpSlider;
    [SerializeField]
    GameObject Fill;
    bool FillIsActive;


    int DropedExp;
    bool IsKilled = false;
    Vector3 PlayerPos;

    private void Start()
    {
        MaxEXP = 100;
        EXP = 0;
        Level = 1;
    }

    private void Update()
    {
        GainExp(); // EXP
        StateControl();
    }

    void GainExp()
    {
        ExpSlider.value = EXP;
        if (ExpSlider.value == 0 && FillIsActive == true) // �ݺ����� ȣ�� ������ bool FillIsActive ���
        {
            Fill.SetActive(false);
            FillIsActive = false;
        }
        else if (ExpSlider.value > 0 && FillIsActive == false)
        {
            Fill.SetActive(true);
            FillIsActive = true;
        }


        if (MaxEXP < EXP)    // ����ġ�� ���� ���� ������
        {
            Level++;
            EXP = EXP - MaxEXP;
            MaxEXP += 100;
        }

        if (IsKilled == true)    // ���͸� ���̸� �����ʿ��� ���� EXP ��ŭ �ø� �� ���� ȣ������� ���� bool IsKilled
        {
            EXP += DropedExp;
            IsKilled = false;
        }
    }

    void StateControl()
    {
        AttackDamage = WeaponDmg + Dmg;
        Defense = ArmorDef + Def;
        Text_Attack.text = AttackDamage.ToString() + "(" + WeaponDmg.ToString() + ")";

        Dmg = Level * 5 + 20;
        Def = Level * 3;
//        Text_Defense.text = Defense.ToString() + "(" + ArmorDef.ToString() + ")";
    }



    #region State Get Set 
    public int PLevel
    {
        get { return Level; }
    }         // ���� 
    public int PDropEXP
    {
        get { return DropedExp; }
        set { DropedExp = value; }
    }       // ����ġ
    public bool PIsKilled
    {
        get { return IsKilled; }
        set { IsKilled = value; }
    }     // ų ����
    public int PHP
    {
        // HP
        get { return HP; }
        set { HP = value; }
    }            // ü��
    public float PSpeed
    {
        // �̵��ӵ�
        get { return Speed; }
        set { Speed = value; }
    }       // �̵��ӵ�
    public float PJump
    {
        // ������
        get { return Jump; }
        set { Jump = value; }
    }        // ������
    public int PIsJump
    {
        // ���� ���°�
        get { return isJump; }
        set { isJump = value; }
    }        // ���� ����
    public bool PIsGround
    {
        // �ٴ� ���� ���°�
        get { return IsGround; }
        set { IsGround = value; }
    }     // �ٴڿ� ��Ҵ��� ����
    public Vector3 PPlayerPos
    {
        // �÷��̾� ������
        get { return PlayerPos; }
        set { PlayerPos = value; }
    } // �÷��̾� ��ġ
    public int PAttackDamage
    {
        // ���ݷ�
        get { return AttackDamage; }
        set { AttackDamage = value; }
    }  // ������

    public int PWeaponDmg
    {
        get { return WeaponDmg; }
        set { WeaponDmg = value; }
    }     // ���� ���ݷ�
    public int PArmorDef
    {
        get { return ArmorDef; }
        set { ArmorDef = value; }
    }      // �� ����
    #endregion
}
