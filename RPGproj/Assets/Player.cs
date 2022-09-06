using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    Animator animator;
    // jumpstate
    float jump_pre;
    float jump_col;

    float Attack_Speed = 0.25f;


    public int CanAttack;


    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jump_pre = gameObject.transform.position.y;
        jump_col = gameObject.transform.position.y;
    }

    void Update()
    {
        GameMgr.GetInstance().SetPlayerPos(transform.position);
        if (CanAttack != 2)
        {
            Move();
        }
        JumpState();
        Attack();
    }

    void Move()
    {

        // 오른쪽 움직임
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigid.velocity += new Vector2(5 * GameMgr.GetInstance().GetSpeed(), 0);
            if (!Input.GetKey(KeyCode.LeftArrow))
            {
                spriteRenderer.flipX = false;
            }
            animator.SetInteger("IsWalking", 1);
        }
        // 왼쪽 움직임
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            rigid.velocity += new Vector2( -5 * GameMgr.GetInstance().GetSpeed(), 0);
            if (!Input.GetKey(KeyCode.RightArrow))
            {
                spriteRenderer.flipX = true;
            }
            animator.SetInteger("IsWalking", 1);
        }
        // 좌우 이동 애니메이션 예외처리
        if ((!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)))
        {
            animator.SetInteger("IsWalking", 0);
            rigid.velocity -= new Vector2(rigid.velocity.x, 0);
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        // 아래 엎드리기
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (GameMgr.GetInstance().GetIsGround() == true)
            {
                animator.SetInteger("IsDown", 1);
            }
        }
        else if (!Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetInteger("IsDown", 0);
        }
        // 점프
        if(Input.GetKeyDown(KeyCode.Space) && GameMgr.GetInstance().GetJumpNow() == 0)
        {
            rigid.AddForce(new Vector2(0, GameMgr.GetInstance().GetJump()));
            animator.SetInteger("IsJump", 1);
            GameMgr.GetInstance().SetJumpNow(1);
        }

        RimitSpeed();
    }

    void JumpState() // 점프 > 하강
    {
        jump_pre = jump_col;
        jump_col = gameObject.transform.position.y;
        if(jump_pre - jump_col > 0.01)
        {
            animator.SetInteger("IsJump", 2);
            GameMgr.GetInstance().SetJumpNow(2);
        }

        // 점프 시 속도 제한
        if(rigid.velocity.y > 11.0f)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 11.0f);
        }
    }

    void Attack()
    {
        if (CanAttack == 0 && Input.GetKeyDown(KeyCode.A))
        {
            animator.SetInteger("IsAttack", 1);
            CanAttack = 1;
            StartCoroutine(Attackable());
        }
    }

    IEnumerator Attackable()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetInteger("IsAttack", 2);
        CanAttack = 2;
        yield return new WaitForSeconds(Attack_Speed);
        animator.SetInteger("IsAttack", 0);
        CanAttack = 0;
    }
    void RimitSpeed()
    {
        if(rigid.velocity.x > 5.0f)
        {
            rigid.velocity -= new Vector2(rigid.velocity.x - 5.0f, 0);
        }
        if (rigid.velocity.x < -5.0f)
        {
            rigid.velocity -= new Vector2(rigid.velocity.x + 5.0f, 0);
        }
    }



    
}
