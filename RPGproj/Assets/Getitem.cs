using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Getitem : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    GameObject player;

    private void Update()
    {
        gameObject.transform.position = player.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.CompareTag("Item") && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("!!!!");
            InventoryMgr.GetInstance().GainItem(collision.transform.GetComponent<Prefab_item>().item);
            Destroy(collision.gameObject); // 주운 아이템 파괴
        }

        // 점프 상태
        if (collision.transform.CompareTag("Ground") && GameMgr.GetInstance().GetJumpNow() == 2)
        {
            animator.SetInteger("IsJump", 0);
            GameMgr.GetInstance().SetJumpNow(0);
        }
    }

    // 바닥 체크
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            GameMgr.GetInstance().SetIsGround(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            GameMgr.GetInstance().SetIsGround(false);
        }
    }
}
