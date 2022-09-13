using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipSlot_Helmet: MonoBehaviour
{
    // 아이템 정보
    Item Item;
    [SerializeField]
    Image ItemImage;
    int ArmorDef;

    private void Update()
    {
        syncDef();
    }

    void syncDef()
    {
        if (Item == null)
            ArmorDef = 0;
        else
            ArmorDef = Item.Defense;

        GameMgr.GetInstance().PArmorDef = ArmorDef;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.TargetSlot != null)
        {
            Slot TargetSlot = DragSlot.instance.TargetSlot;

            RegistSlot(TargetSlot);
        }
    }
    public void RegistSlot(Slot _slot)
    {
        if (_slot.item.equiptype == Item.EquipType.Helmet && Item == null)        // 장비칸이 비어있을 경우
        {
            if (_slot.item.RequiredLevel <= GameMgr.GetInstance().PLevel)
            {
                Item = _slot.item;                                         // 아이템에 정보 등록
                ItemImage.sprite = _slot.item.ItemImage;                   // 이미지 정보 등록 및 불투명화
                SetColor(1);
                _slot.UpdateSlotCount(-1);                                 // 인벤토리 슬롯에서 삭제
            }
            else
            {
                Debug.Log("레벨이 부족합니다." + _slot.item.RequiredLevel + "이상이 되어야 착용할 수 있습니다.");
            }

        }
        else if (_slot.item.equiptype == Item.EquipType.Helmet && Item != null)   // 장비칸이 비어있지 않을 경우
        {
            Item tempItem = Item;                           // 아이템 정보 임시 저장

            Item = _slot.item;
            ItemImage.sprite = _slot.item.ItemImage;
            SetColor(1);
            _slot.UpdateSlotCount(-1);

            _slot.AddItem(tempItem);                   // 기존 아이템 인벤토리에 반환
        }
        else
        {
            Debug.Log("무기만 장착 할 수 있습니다.");
        }
    }




    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)     // 우클릭 시
        {
            InventoryMgr.GetInstance().GainItem(Item);      // 아이템 인벤토리에 추가
            ClearSlot();                                    // 장비칸 클리어
        }
    }
    void ClearSlot()
    {
        Item = null;
        ItemImage.sprite = null;
        SetColor(0);
    }

    void SetColor(float _alpha) // 아이템 아이콘 투명도 조절
    {
        Color color = ItemImage.color;
        color.a = _alpha;
        ItemImage.color = color;
    }
}