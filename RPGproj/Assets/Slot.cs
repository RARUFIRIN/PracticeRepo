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


    private void SetColor(float _alpha) // 아이템 아이콘 투명도 조절
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddItem(Item _item, int _count = 1) // 아이템 추가
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if(item.itemType != Item.ItemType.Weapon && item.itemType != Item.ItemType.armor) // 무기가 아니면 갯수표시를 활성화
        {
            image_count.SetActive(true);
            text_count.text = itemCount.ToString();
        }
        else // 무기 일 때 갯수표시를 비활성화
        {
            text_count.text = "0";
            image_count.SetActive(false);
        }

        SetColor(1); // 아이템 이미지 불투명화
    }

    public void UpdateSlotCount(int _count) // 아이템 갯수 업데이트
    {
        itemCount += _count;
        text_count.text = itemCount.ToString();

        if(itemCount <= 0)
        {
            ClearSlot();
        }
    }

    private void ClearSlot() // 슬롯 정보 초기화
    {
        // 각종 정보 초기화
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0); // 해당 아이템 이미지 투명화

        text_count.text = "0";
        image_count.SetActive(false); // 아이템 갯수표시를 비활성화

    }
}
