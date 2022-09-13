using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    // 게임매니저 싱글톤 //
    private GameMgr() { }
    private static GameMgr instance = null;
    public static GameMgr GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("오브젝트를 찾을 수 없습니다.");
        }
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }

    // 플레이어 상태값
    int Dmg;                // 기본 데미지
    int AttackDamage;       // 최종 데미지

    int Def;                // 기본 방어력
    int Defense;            // 최종 방어력

    [SerializeField]
    TextMeshProUGUI Text_Attack;
    [SerializeField]
    TextMeshProUGUI Text_Defense;


    float Speed = 1.0f;     // 이동속도
    float Jump = 400.0f;    // 점프력
    int isJump = 0;         // 0 = 점프 가능한 상태 1 = 점프중(상승) 2 = 점프중(하강)
    bool IsGround;          // 바닥과 닿아있는지 체크

    // 장비 추가능력치
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
        if (ExpSlider.value == 0 && FillIsActive == true) // 반복적인 호출 방지용 bool FillIsActive 사용
        {
            Fill.SetActive(false);
            FillIsActive = false;
        }
        else if (ExpSlider.value > 0 && FillIsActive == false)
        {
            Fill.SetActive(true);
            FillIsActive = true;
        }


        if (MaxEXP < EXP)    // 경험치가 가득 차면 레벨업
        {
            Level++;
            EXP = EXP - MaxEXP;
            MaxEXP += 100;
        }

        if (IsKilled == true)    // 몬스터를 죽이면 몬스토쪽에서 보낸 EXP 만큼 올린 후 이후 호출방지를 위한 bool IsKilled
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
    }         // 레벨 
    public int PDropEXP
    {
        get { return DropedExp; }
        set { DropedExp = value; }
    }       // 경험치
    public bool PIsKilled
    {
        get { return IsKilled; }
        set { IsKilled = value; }
    }     // 킬 여부
    public int PHP
    {
        // HP
        get { return HP; }
        set { HP = value; }
    }            // 체력
    public float PSpeed
    {
        // 이동속도
        get { return Speed; }
        set { Speed = value; }
    }       // 이동속도
    public float PJump
    {
        // 점프력
        get { return Jump; }
        set { Jump = value; }
    }        // 점프력
    public int PIsJump
    {
        // 점프 상태값
        get { return isJump; }
        set { isJump = value; }
    }        // 점프 여부
    public bool PIsGround
    {
        // 바닥 접촉 상태값
        get { return IsGround; }
        set { IsGround = value; }
    }     // 바닥에 닿았는지 여부
    public Vector3 PPlayerPos
    {
        // 플레이어 포지션
        get { return PlayerPos; }
        set { PlayerPos = value; }
    } // 플레이어 위치
    public int PAttackDamage
    {
        // 공격력
        get { return AttackDamage; }
        set { AttackDamage = value; }
    }  // 데미지

    public int PWeaponDmg
    {
        get { return WeaponDmg; }
        set { WeaponDmg = value; }
    }     // 무기 공격력
    public int PArmorDef
    {
        get { return ArmorDef; }
        set { ArmorDef = value; }
    }      // 방어구 방어력
    #endregion
}
