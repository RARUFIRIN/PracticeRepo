using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    int AttackDamage;
    float Speed = 1.0f; // �̵��ӵ�
    float Jump = 400.0f;  // ������
    int isJump = 0; // 0 = ���� ������ ���� 1 = ������(���) 2 = ������(�ϰ�)
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
        // �̵��ӵ�
        get { return Speed; }
        set { Speed = value; }
    }

    public float PJump
    {
        // ������
        get { return Jump; }
        set { Jump = value; }
    }

    public int PIsJump
    {
        // ���� ���°�
        get { return isJump; }
        set { isJump = value; }
    }

    public bool PIsGround
    {
        // �ٴ� ���� ���°�
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
