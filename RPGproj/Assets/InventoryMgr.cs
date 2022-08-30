using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMgr : MonoBehaviour
{
    // �κ��丮 ������Ƽ �̱��� //
    static InventoryMgr m_Instance = null;
    public static InventoryMgr Instance
    {
        get
        {
            if (m_Instance == null) m_Instance = new InventoryMgr();
            return m_Instance;
        }
    }

    bool B_inventoryActive = false;

    [SerializeField]
    GameObject InventoryUI; // �κ��丮
    [SerializeField]
    GameObject SlotParent;  // �׸���

    Slot[] slots; // ���� �迭

    private void Start()
    {
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
    }
}
