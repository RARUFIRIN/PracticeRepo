using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickItem : MonoBehaviour, IDropHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField]
    private TextMeshProUGUI text_count;
    [SerializeField]
    private GameObject image_count;
    [SerializeField]
    private GameObject Parent;

    QuickItem[] QuickSlots;
    Slot LinkedSlot;
    private void Awake()
    {
        QuickSlots = Parent.GetComponentsInChildren<QuickItem>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.instance.TargetSlot != null)
        {
            RegistSlot();
        }
    }

    private void RegistSlot()
    {
        for (int i = 0; i < QuickSlots.Length; i++)
        {
            if (QuickSlots[i].item != null && QuickSlots[i].item == DragSlot.instance.TargetSlot.item)
            {
                ClearSlot(QuickSlots[i]);
            }
        }

        ClearSlot(this);
        
        LinkedSlot = DragSlot.instance.TargetSlot; // 슬롯정보 저장해두고 소비 및 획득 동기화 구현해야함
        item = DragSlot.instance.TargetSlot.item;
        if(DragSlot.instance.TargetSlot.itemCount > 0)
        {
            image_count.SetActive(true);
            itemCount = DragSlot.instance.TargetSlot.itemCount;
        }
    }

    private void ClearSlot(QuickItem _s)
    {
        _s.item = null;
        _s.itemCount = 0;
        _s.itemImage = null;
    }
}

