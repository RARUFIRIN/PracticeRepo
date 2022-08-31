using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigid;

    // jumpstate
    float jump_pre;
    float jump_col;
    void Awake()
    {
        jump_pre = gameObject.transform.position.y;
        jump_col = gameObject.transform.position.y;
    }

    void Update()
    {
        Move();
        JumpState();

            Debug.Log(GameMgr.GetInstance().GetJumpNow().ToString());

    }

    void Move()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += GameMgr.GetInstance().GetSpeed() * Time.deltaTime * new Vector3(2.0f, 0, 0);
        }
        
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += GameMgr.GetInstance().GetSpeed() * Time.deltaTime * new Vector3(-2.0f, 0, 0);
        }
        
        if(Input.GetKey(KeyCode.DownArrow))
        {
            
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && GameMgr.GetInstance().GetJumpNow() == 0)
        {
            rigid.AddForce(new Vector2(0, GameMgr.GetInstance().GetJump()));
            GameMgr.GetInstance().SetJumpNow(1);
        }
    }

    void JumpState()
    {
        jump_pre = jump_col;
        jump_col = gameObject.transform.position.y;
        if(jump_pre - jump_col > 0.2f)
        {
            GameMgr.GetInstance().SetJumpNow(2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Ground") && GameMgr.GetInstance().GetJumpNow() == 2)
        {
            GameMgr.GetInstance().SetJumpNow(0);
        }
    }
}
