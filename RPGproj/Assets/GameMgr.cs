using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    int AttackDamage;
    float Speed = 1.0f; // 이동속도
    float Jump = 400.0f;  // 점프력
    int isJump = 0; // 0 = 점프 가능한 상태 1 = 점프중(상승) 2 = 점프중(하강)
    bool IsGround;

    int MaxHP;
    int HP;

    int MaxMP;
    int MP;

    int MaxEXP;
    int EXP;

    Vector3 PlayerPos;


    public int PHP
    {
        // HP
        get { return HP; }
        set { HP = value; }
    }
    public float PSpeed
    {
        // 이동속도
        get { return Speed; }
        set { Speed = value; }
    }

    public float PJump
    {
        // 점프력
        get { return Jump; }
        set { Jump = value; }
    }

    public int PIsJump
    {
        // 점프 상태값
        get { return isJump; }
        set { isJump = value; }
    }

    public bool PIsGround
    {
        // 바닥 접촉 상태값
        get { return IsGround;}
        set { IsGround = value;}
    }

    public void SetPlayerPos(Vector3 _pos)
    {
        PlayerPos = _pos;
    }
    public Vector3 GetPlayerPos()
    {
        return PlayerPos;
    }
    public void SetAttackDamage(int _i)
    {
        AttackDamage = _i;
    }
    public int GetAttackDamage()
    {
        return AttackDamage;
    }
}
