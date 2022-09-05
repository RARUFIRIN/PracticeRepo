using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    int IsAttack;
    int IsMove;
    int IsDie;
    float HP;
    float Speed = 1.5f;
    MonsterState State;

    RaycastHit2D hit;
    float RayDist = 5.0f;
    
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        StartCoroutine(MoveTime());
        State = MonsterState.IDLE;
    }
    private void Update()
    {
        Debug.Log(IsMove);
        if (State == MonsterState.IDLE)
        {
            MoveState();
        }
        RayCast();
        
    }

    enum MonsterState
    {
        IDLE,
        Trace,
        Die,
        Damaged,
    }


    void MoveState()
    {
        switch (IsMove)
        {
            case 0:
                {
                    animator.SetInteger("IsWalk", 0);
                    rigid.velocity -= new Vector2(rigid.velocity.x, 0);
                }
                break;
            case 1:
                {
                    rigid.velocity -= new Vector2(rigid.velocity.x, 0);
                    animator.SetInteger("IsWalk", 1);
                    rigid.velocity += new Vector2(-Speed, 0);
                    spriteRenderer.flipX = true;
                }
                break;
            case 2:
                {
                    rigid.velocity -= new Vector2(rigid.velocity.x, 0);
                    animator.SetInteger("IsWalk", 1);
                    rigid.velocity += new Vector2(Speed, 0);
                    spriteRenderer.flipX = false;

                }
                break;
        }
    }

    IEnumerator MoveTime()
    {
        yield return new WaitForSeconds(3.0f);
        IsMove = Random.Range(0, 3); // 0 idle 1 left 2 right
        StartCoroutine(MoveTime());
    }

    void RayCast()
    {
        Debug.DrawRay(transform.position - new Vector3(RayDist / 2, -0.5f, 0), new Vector3(1, 0, 0) * RayDist, Color.green);
        hit = Physics2D.Raycast(transform.position - new Vector3(RayDist / 2, -0.5f, 0), new Vector3(1, 0, 0) * RayDist);
        if (hit)
        {
            Debug.Log("¹ß°¢µÊ");
        }
    }
    void Trace()
    {

    }
}
