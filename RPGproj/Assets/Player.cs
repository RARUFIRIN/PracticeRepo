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
    int CanJump;

    float Attack_Speed = 1;


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
        Move();
        JumpState();
        Attack();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += GameMgr.GetInstance().GetSpeed() * Time.deltaTime * new Vector3(2.0f, 0, 0);
            if (!Input.GetKey(KeyCode.LeftArrow))
            {
                spriteRenderer.flipX = false;
            }
            animator.SetInteger("IsWalking", 1);
        }
        
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += GameMgr.GetInstance().GetSpeed() * Time.deltaTime * new Vector3(-2.0f, 0, 0);
            if (!Input.GetKey(KeyCode.RightArrow))
            {
                spriteRenderer.flipX = true;
            }
            animator.SetInteger("IsWalking", 1);
        }

        if ((!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)))
        {
            animator.SetInteger("IsWalking", 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && CanJump == 0)
        {
            rigid.AddForce(new Vector2(0, GameMgr.GetInstance().GetJump()));
            animator.SetInteger("IsJump", 1);
            CanJump = 1;
        }
    }

    void JumpState() // 점프 > 하강
    {
        jump_pre = jump_col;
        jump_col = gameObject.transform.position.y;
        if(jump_pre - jump_col > 0)
        {
            animator.SetInteger("IsJump", 2);
            CanJump = 2;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground") && CanJump == 2)
        {
            animator.SetInteger("IsJump", 0);
            CanJump = 0;
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
        yield return new WaitForSeconds(Attack_Speed);
        animator.SetInteger("IsAttack", 0);
        CanAttack = 0;
    }
}
