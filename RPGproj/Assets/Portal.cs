using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Portal : MonoBehaviour
{


    public bool NowChange = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (GameMgr.GetInstance().PTryPortal == true && NowChange == false)
            {
                NowChange = true;
                StartCoroutine(GameMgr.GetInstance().FadeOutStart());
            }
        }
    }


}
