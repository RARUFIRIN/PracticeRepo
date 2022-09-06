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

    float jump_pre;
    float jump_col;
    
    float RayDist = 5.0f;

    Vector2 dir = Vector2.right;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        jump_pre = gameObject.transform.position.y;
        jump_col = gameObject.transform.position.y;
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
                    JumpState();
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
                    rigid.velocity -= new Vector2(rigid.velocity.x, rigid.velocity.y);
                }
                break;
            case 1:
                {
                    Move(-1);
                }
                break;
            case 2:
                {
                    Move(1);
                }
                break;
        }
    }

    void JumpState() // 점프 > 하강
    {
        jump_pre = jump_col;
        jump_col = gameObject.transform.position.y;
        if (jump_pre - jump_col > 0.01)
        {
            animator.SetInteger("IsAttack", 2);
        }

        // 점프 시 속도 제한
        if (rigid.velocity.y > 11.0f)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 11.0f);
        }
    }

    IEnumerator MoveTime()
    {
        yield return new WaitForSeconds(3.0f);
        IsMove = Random.Range(0, 3); // 0 idle 1 left 2 right
        StartCoroutine(MoveTime());
    }
    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(3.0f);
        IsAttack = 0;
    }
    
    void Move(int _i /* -1 left 1 right*/)
    {
        rigid.velocity -= new Vector2(rigid.velocity.x, 0);
        animator.SetInteger("IsWalk", 1);
        rigid.velocity += new Vector2(Speed * _i, 0);
        if (_i == -1)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void Attack(float _f /* -1 Left 1 Right */)
    {
        if (IsAttack == 0)
        {
            IsAttack = 1;
            rigid.AddForce(new Vector2(100 * _f, 300));
            animator.SetInteger("IsAttack", 1);
            StartCoroutine(AttackTime());
        }
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
        if(Vector3.Distance(transform.position, GameMgr.GetInstance().GetPlayerPos()) < RayDist * 2)
        {
            if (Vector3.Distance(transform.position, GameMgr.GetInstance().GetPlayerPos()) > 0 && IsAttack == 2)
            {
                if (GameMgr.GetInstance().GetPlayerPos().x > transform.position.x)
                {
                    Move(1);
                }

                if (GameMgr.GetInstance().GetPlayerPos().x < transform.position.x)
                {
                    Move(-1);
                }
            }
            if(Vector3.Distance(transform.position, GameMgr.GetInstance().GetPlayerPos()) < RayDist / 2)
            {
                if (GameMgr.GetInstance().GetPlayerPos().x > transform.position.x)
                {
                    Attack(1);
                }

                if (GameMgr.GetInstance().GetPlayerPos().x < transform.position.x)
                {
                    Attack(-1);
                }
            }
        }
        else
        {
            State = MonsterState.IDLE;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground") && IsAttack == 1)
        {
            IsAttack = 2;
            animator.SetInteger("IsAttack", 0);
        }
    }
}
