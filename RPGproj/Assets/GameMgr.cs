using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    // �κ��丮 �̱��� //
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
    float f_Speed = 1.0f; // �̵��ӵ�
    float f_Jump = 400.0f;  // ������
    int isJump = 0; // 0 = ���� ������ ���� 1 = ������(���) 2 = ������(�ϰ�)
    bool IsGround;

    Vector3 PlayerPos;

    public void SetSpeed(float _speed)
    {
        f_Speed = _speed;
    }
    public float GetSpeed()
    {
        return f_Speed;
    }
    public void SetJump(float _jump)
    {
        f_Jump = _jump;
    }
    public float GetJump()
    {
        return f_Jump;
    }
    public int GetJumpNow()
    {
        return isJump;
    }
    public void SetJumpNow(int _state)
    {
        isJump = _state;
    }
    public bool GetIsGround()
    {
        return IsGround;
    }
    public void SetIsGround(bool _state)
    {
        IsGround = _state;
    }
    public void SetPlayerPos(Vector3 _pos)
    {
        PlayerPos = _pos;
    }
    public Vector3 GetPlayerPos()
    {
        return PlayerPos;
    }
}
