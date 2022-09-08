using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMgr : MonoBehaviour
{
    // 인벤토리 싱글톤 //
    private InventoryMgr() { } 
    private static InventoryMgr instance = null;
    public static InventoryMgr GetInstance()
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

    bool B_inventoryActive = false;

    [SerializeField]
    GameObject InventoryUI; // 인벤토리
    [SerializeField]
    GameObject SlotParent;  // 그리드

    public Slot[] slots; // 슬롯 배열

    // bool InventoryIsFull;

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

    public void GainItem(Item _item, int _count = 1) // 아이템 획득
    {
        if(Item.ItemType.Weapon != _item.itemType && Item.ItemType.armor != _item.itemType)
        { 
            for(int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null) // 예외처리
                {
                    if (slots[i].item.itemName == _item.itemName) // 같은 아이템이 있을 시 (장비 제외) 갯수를 추가해준다.
                    {
                        slots[i].UpdateSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for(int i = 0; i < slots.Length; i++) // 같은 아이템이 없을 시 빈 칸에 아이템 추가
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }

        // 인벤토리 가득 참. (구현해야함)
        // InventoryIsFull = true;
    }
}
