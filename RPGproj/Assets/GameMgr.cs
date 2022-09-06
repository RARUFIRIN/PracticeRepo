using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    // 인벤토리 싱글톤 //
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
    float f_Speed = 1.0f; // 이동속도
    float f_Jump = 400.0f;  // 점프력
    int isJump = 0; // 0 = 점프 가능한 상태 1 = 점프중(상승) 2 = 점프중(하강)
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
