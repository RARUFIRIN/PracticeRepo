using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMgr : MonoBehaviour
{
    // ����Ʈ�Ŵ��� �̱��� //
    private QuestMgr() { }
    private static QuestMgr instance = null;
    public static QuestMgr GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("������Ʈ�� ã�� �� �����ϴ�.");
        }
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }




}
