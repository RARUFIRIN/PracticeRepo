using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField]
    private TextMeshProUGUI text_count;
    [SerializeField]
    private GameObject image_count;


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
