using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField]
    private TextMeshProUGUI text_count;
    [SerializeField]
    private GameObject image_count;
    [SerializeField]
    private Image DragSlotimg;
    #region �κ��丮 ��ȣ�ۿ�
    public void OnPointerClick(PointerEventData eventData)      // ���콺 ��Ŭ�� �̺�Ʈ
    {
        if(eventData.button == PointerEventData.InputButton.Right)

        {
            if(item != null)
            {
                if(item.itemType == Item.ItemType.Weapon)
                {
                    // ���� ��ü or ���� ����

                }
                else if (item.itemType == Item.ItemType.armor)
                {
                    // �� ��ü or �� ����

                }
                else if (item.itemType == Item.ItemType.Food)
                {
                    // �Һ������ �Һ�
                    Debug.Log(item.itemName + "�� ����߽��ϴ�.");
                    UpdateSlotCount(-1);
                }
            }
        }
        Debug.Log("��Ŭ��!");
    }

    public void OnBeginDrag(PointerEventData eventData)         // �巡�� ���� �̺�Ʈ
    {                                                           // �ش� ������ ������ �̹����� �Ҵ�
        if(item != null)
        {
            DragSlot.instance.TargetSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)          // �巡�� �� �̹����� �����͸� ����ٴϰ� ��
    {
       if(item != null)
       DragSlot.instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)       // �巡�� ���� �̺�Ʈ
    {
        DragSlot.instance.SetColor(0);                      // �̹��� ����ȭ
        DragSlot.instance.TargetSlot = null;                // Ÿ�� ���� �ʱ�ȭ
    }



    public void OnDrop(PointerEventData eventData)          // �巡�� �� ��� �̺�Ʈ
    {
        if (DragSlot.instance.TargetSlot != null)           // ����� ���԰� ��ü
            ChangeSlot();
    }

    private void ChangeSlot()
    {
        Item tempitem = item;                               // �ӽð�ü�� ���� ����
        int tempitemCount = itemCount;

        AddItem(DragSlot.instance.TargetSlot.item, DragSlot.instance.TargetSlot.itemCount); // ����� ���Կ� ������ �߰�

        if (tempitem != null)
            DragSlot.instance.TargetSlot.AddItem(tempitem, tempitemCount);      // ����� ���Կ� �������� �־��ٸ� �巡�� ���� ���Կ� �߰�
        else
            DragSlot.instance.TargetSlot.ClearSlot();                           // �����ٸ� �巡�� ���� ���� Ŭ����
    }

    #endregion

    private void SetColor(float _alpha) // ������ ������ ���� ����
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddItem(Item _item, int _count = 1) // ������ �߰�
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if(item.itemType != Item.ItemType.Weapon && item.itemType != Item.ItemType.armor) // ���Ⱑ �ƴϸ� ����ǥ�ø� Ȱ��ȭ
        {
            image_count.SetActive(true);
            text_count.text = itemCount.ToString();
        }
        else // ���� �� �� ����ǥ�ø� ��Ȱ��ȭ
        {
            text_count.text = "0";
            image_count.SetActive(false);
        }

        SetColor(1); // ������ �̹��� ������ȭ
    }

    public void UpdateSlotCount(int _count) // ������ ���� ������Ʈ
    {
        itemCount += _count;
        text_count.text = itemCount.ToString();

        if(itemCount <= 0)
        {
            ClearSlot();
        }
    }

    private void ClearSlot() // ���� ���� �ʱ�ȭ
    {
        // ���� ���� �ʱ�ȭ
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0); // �ش� ������ �̹��� ����ȭ

        text_count.text = "0";
        image_count.SetActive(false); // ������ ����ǥ�ø� ��Ȱ��ȭ

    }
}
