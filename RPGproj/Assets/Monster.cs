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

    float RayDist = 5.0f;

    Vector2 dir = Vector2.right;
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
        Debug.Log(State);
        switch (State)
        {
            case MonsterState.IDLE:
                {
                    MoveState();
                    RayCast();
                }
                break;
            case MonsterState.Trace:
                {
                    Trace();

                }
                break;
            case MonsterState.Die:
                {

                }
                break;
            case MonsterState.Damaged:
                {

                }
                break;
            default:
                break;
        }
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
                    MoveLeft();
                }
                break;
            case 2:
                {
                    MoveRight();
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
    
    void MoveLeft()
    {
        rigid.velocity -= new Vector2(rigid.velocity.x, 0);
        animator.SetInteger("IsWalk", 1);
        rigid.velocity += new Vector2(-Speed, 0);
        spriteRenderer.flipX = true;
    }
    void MoveRight()
    {
        rigid.velocity -= new Vector2(rigid.velocity.x, 0);
        animator.SetInteger("IsWalk", 1);
        rigid.velocity += new Vector2(Speed, 0);
        spriteRenderer.flipX = false;
    }
    void RayCast()
    {
        Vector2 MosterPos = new Vector2(transform.position.x, transform.position.y);

        Debug.DrawRay(MosterPos - new Vector2(RayDist / 2, -0.5f),dir * RayDist, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(MosterPos - new Vector2(RayDist / 2 , - 0.5f), dir, RayDist, LayerMask.GetMask("Player"));
        if (hit.collider != null && hit.transform.CompareTag("Player"))
        {
            
            State = MonsterState.Trace;
            Debug.Log(hit);
        }
    }
    void Trace()
    {
        if(Vector3.Distance(transform.position, GameMgr.GetInstance().GetPlayerPos()) < RayDist)
        {
            //if (Vector3.Distance(transform.position, GameMgr.GetInstance().GetPlayerPos()) > RayDist / 2)
            //{
                if (GameMgr.GetInstance().GetPlayerPos().x > transform.position.x)
                {
                    MoveRight();
                }

                if (GameMgr.GetInstance().GetPlayerPos().x < transform.position.x)
                {
                    MoveLeft();
                }
            //}
        }
    }
}
