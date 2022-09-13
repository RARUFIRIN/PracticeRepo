using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipSlot_Helmet: MonoBehaviour
{
    // ������ ����
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
        if (_slot.item.equiptype == Item.EquipType.Helmet && Item == null)        // ���ĭ�� ������� ���
        {
            if (_slot.item.RequiredLevel <= GameMgr.GetInstance().PLevel)
            {
                Item = _slot.item;                                         // �����ۿ� ���� ���
                ItemImage.sprite = _slot.item.ItemImage;                   // �̹��� ���� ��� �� ������ȭ
                SetColor(1);
                _slot.UpdateSlotCount(-1);                                 // �κ��丮 ���Կ��� ����
            }
            else
            {
                Debug.Log("������ �����մϴ�." + _slot.item.RequiredLevel + "�̻��� �Ǿ�� ������ �� �ֽ��ϴ�.");
            }

        }
        else if (_slot.item.equiptype == Item.EquipType.Helmet && Item != null)   // ���ĭ�� ������� ���� ���
        {
            Item tempItem = Item;                           // ������ ���� �ӽ� ����

            Item = _slot.item;
            ItemImage.sprite = _slot.item.ItemImage;
            SetColor(1);
            _slot.UpdateSlotCount(-1);

            _slot.AddItem(tempItem);                   // ���� ������ �κ��丮�� ��ȯ
        }
        else
        {
            Debug.Log("���⸸ ���� �� �� �ֽ��ϴ�.");
        }
    }




    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)     // ��Ŭ�� ��
        {
            InventoryMgr.GetInstance().GainItem(Item);      // ������ �κ��丮�� �߰�
            ClearSlot();                                    // ���ĭ Ŭ����
        }
    }
    void ClearSlot()
    {
        Item = null;
        ItemImage.sprite = null;
        SetColor(0);
    }

    void SetColor(float _alpha) // ������ ������ ���� ����
    {
        Color color = ItemImage.color;
        color.a = _alpha;
        ItemImage.color = color;
    }
}