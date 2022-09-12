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
    public int HP;
    float Speed = 1.5f;
    MonsterState State;

    float jump_pre;
    float jump_col;
    
    float RayDist = 5.0f;

    Vector2 dir = Vector2.right;
    private void Awake()
    {
        HP = 100;

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
        if (IsAttack == 1)
        {
            StartCoroutine(AttackTime());
        }

        if(HP <= 0)
        {
            State = MonsterState.Die;
        }
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
            case MonsterState.Damaged:
                {

                }
                break;
            case MonsterState.Die:
                {
                    StartCoroutine(DieMotion());
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
        Damaged,
        Die,
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
        IsAttack = 2;
        yield return new WaitForSeconds(3.0f);
        IsAttack = 0;
    }
    
    IEnumerator ChangeState(float _f, MonsterState _S) // 상태변화 딜레이
    {
        yield return new WaitForSeconds(_f);
        State = _S;
    }

    IEnumerator DieMotion()
    {
        animator.SetInteger("IsDie", 1);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }


    void Damaged()
    {
        State = MonsterState.Damaged;
        animator.SetInteger("IsWalk", 0);
        animator.SetInteger("IsAttack", 0);
        rigid.velocity = Vector3.zero;
        if (GameMgr.GetInstance().GetPlayerPos().x < transform.position.x)
        {
            rigid.AddForce(new Vector2(40, 100));
        }
        else
        {
            rigid.AddForce(new Vector2(-40, 100));
        }
        HP -= GameMgr.GetInstance().GetAttackDamage();
        StartCoroutine(ChangeState(1.5f, MonsterState.Trace));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("AttackBox") && State != MonsterState.Damaged)
        {
            Damaged();
        }
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
            rigid.AddForce(new Vector2(70 * _f, 200));
            animator.SetInteger("IsAttack", 1);
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
            if (Vector3.Distance(transform.position, GameMgr.GetInstance().GetPlayerPos()) > 2.5f && IsAttack != 1)
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
        if (collision.transform.CompareTag("Ground") && (IsAttack != 1 || State == MonsterState.Damaged))
        {
            animator.SetInteger("IsAttack", 0);

            rigid.velocity = Vector3.zero; // 바닥에 닿으면 정지
        }
    }
}
