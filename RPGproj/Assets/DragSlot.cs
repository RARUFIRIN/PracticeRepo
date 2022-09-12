using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;
    public Slot TargetSlot;
    [SerializeField]
    Image itemImage;

    void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image _img)
    {
        itemImage.sprite = _img.sprite;
        SetColor(1);
    }
    
    public void SetColor(float _f)
    {
        Color color = itemImage.color;
        color.a = _f;
        itemImage.color = color;
    }
    public Sprite GetImage()
    {
        return itemImage.sprite;
    }
}
