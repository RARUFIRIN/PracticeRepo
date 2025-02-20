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

    Slot PreSlot, CurSlot;
    // 퀵슬롯 동기화 용 슬롯

    [SerializeField]
    GameObject EquipSlotParent; // 장비슬롯
    [SerializeField]
    EquipSlot_Weapon EquipSlot_Weapon;
    [SerializeField]
    EquipSlot_Helmet EquipSlot_Helmet;
    [SerializeField]
    EquipSlot_Armor EquipSlot_Armor;
    [SerializeField]
    EquipSlot_Boot EquipSlot_Boot;
    bool B_EquipSlotActive = false;


    private void Start()
    {
        PreSlot = GetComponent<Slot>();
        CurSlot = GetComponent<Slot>();
        slots = SlotParent.GetComponentsInChildren<Slot>();
    }

    private void Update()
    {
        OpenInventory();
        OpenEquipSlot();
    }

    public void Equip(Item.EquipType _Type, Slot _slot)
    {
        switch (_Type)
        {
            case Item.EquipType.Weapon:
                {
                    EquipSlot_Weapon.RegistSlot(_slot);
                }
                break;
            case Item.EquipType.Armor:
                {
                        EquipSlot_Armor.RegistSlot(_slot);
                }
                break;
            case Item.EquipType.Helmet:
                {
                        EquipSlot_Helmet.RegistSlot(_slot);
                }
                break;
            case Item.EquipType.Boot:
                {
                    EquipSlot_Boot.RegistSlot(_slot);
                }
                break;
            default:
                break;
        }
    }
    private void OpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            B_inventoryActive = !B_inventoryActive;
            InventoryUI.SetActive(B_inventoryActive);
        }
    }

    private void OpenEquipSlot()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            B_EquipSlotActive = !B_EquipSlotActive;
            EquipSlotParent.SetActive(B_EquipSlotActive);
        }
    }

    public void GainItem(Item _item, int _count = 1) // 아이템 획득
    {
        if(Item.ItemType.Equip != _item.itemType)
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

    public void SetChangedSlots(Slot _s1, Slot _s2)
    {
        PreSlot = _s1;
        CurSlot = _s2;
    }

    public Slot GetChangedSlot(int i)// 0은 PreSlot을 1은 CurSlot 을 반환
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
