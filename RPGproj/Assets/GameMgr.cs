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
}
