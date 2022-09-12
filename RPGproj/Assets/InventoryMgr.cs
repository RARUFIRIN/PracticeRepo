using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMgr : MonoBehaviour
{
    // �κ��丮 �̱��� //
    private InventoryMgr() { } 
    private static InventoryMgr instance = null;
    public static InventoryMgr GetInstance()
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

    bool B_inventoryActive = false;

    [SerializeField]
    GameObject InventoryUI; // �κ��丮
    [SerializeField]
    GameObject SlotParent;  // �׸���

    public Slot[] slots; // ���� �迭

    // bool InventoryIsFull;

    Slot PreSlot, CurSlot;
    // ������ ����ȭ �� ����

    private void Start()
    {
        PreSlot = GetComponent<Slot>();
        CurSlot = GetComponent<Slot>();
        slots = SlotParent.GetComponentsInChildren<Slot>();
    }

    private void Update()
    {
        OpenInventory();
    }

    private void OpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            B_inventoryActive = !B_inventoryActive;

            if (B_inventoryActive)
                OpenInventoryUI();
            else
                CloseInventoryUI();
        }
    }

    private void OpenInventoryUI()
    {
        InventoryUI.SetActive(true);
    }

    private void CloseInventoryUI()
    {
        InventoryUI.SetActive(false);
    }

    public void GainItem(Item _item, int _count = 1) // ������ ȹ��
    {
        if(Item.ItemType.Weapon != _item.itemType && Item.ItemType.armor != _item.itemType)
        { 
            for(int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null) // ����ó��
                {
                    if (slots[i].item.itemName == _item.itemName) // ���� �������� ���� �� (��� ����) ������ �߰����ش�.
                    {
                        slots[i].UpdateSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for(int i = 0; i < slots.Length; i++) // ���� �������� ���� �� �� ĭ�� ������ �߰�
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }

        // �κ��丮 ���� ��. (�����ؾ���)
        // InventoryIsFull = true;
    }

    public void InstedUpdateSlotCount(Slot _s)
    {
        _s.UpdateSlotCount(-1);
    }    

    public void SetChangedSlots(Slot _s1, Slot _s2)
    {
        PreSlot = _s1;
        CurSlot = _s2;
    }

    public Slot GetChangedSlot(int i)/* 0�� PreSlot�� 1�� CurSlot �� ��ȯ�մϴ�.)*/
    {
        if (i == 0)
        {
            return PreSlot;
        }
        else if(i == 1)
        {
            return CurSlot;
        }
        else
        {
            return null;
        }
    }
}
