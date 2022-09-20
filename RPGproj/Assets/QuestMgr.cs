using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMgr : MonoBehaviour
{
    // 퀘스트매니저 싱글톤 //
    private QuestMgr() { }
    private static QuestMgr instance = null;
    public static QuestMgr GetInstance()
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




}
