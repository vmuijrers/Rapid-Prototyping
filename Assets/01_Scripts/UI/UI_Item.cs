using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RawImage uiImage { get; private set; }
    public Item itemReference { get; private set; }
    public UI_ItemSlot currentSlot { get; private set; }

    private void Awake()
    {
        uiImage = GetComponent<RawImage>();
    }

    public void Setup(Item item)
    {
        itemReference = item;
        itemReference.gameObject.SetActive(false);
        itemReference.OnUseEvent += OnUseItem;
        uiImage.texture = item.icon;
    }

    public void DropItem(Vector3 dropPosition)
    {
        itemReference.gameObject.SetActive(true);
        itemReference.gameObject.transform.position = dropPosition; 
        currentSlot.ReleaseItem();
        currentSlot = null;
    }

    public void SetSlot(UI_ItemSlot slot)
    {
        currentSlot = slot;
    }

    private void OnUseItem(Item usedItem)
    {
        if (usedItem.isConsumable)
        {
            currentSlot.ReleaseItem();
            Destroy(gameObject);
        }
    }

    public void Use()
    {
        itemReference.Use();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        uiImage.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var list = eventData.hovered;
        UI_ItemSlot dropSlot = null;
        foreach(var v in list)
        {
            var possibleSlot = v.GetComponent<UI_ItemSlot>();
            if (possibleSlot != null)
            {
                dropSlot = possibleSlot;
                break;
            }
        }
        if(dropSlot != null)
        {
            var otherItem = dropSlot.item;
            currentSlot.ObtainItem(otherItem);
            dropSlot.ObtainItem(this);
            uiImage.raycastTarget = true;
        }
        else
        {
            //Drop the item
            var pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.01f);
            Ray r = Camera.main.ScreenPointToRay(pos);
            if (Physics.Raycast(r, out RaycastHit hitinfo))
            {
                DropItem(hitinfo.point);
                Destroy(gameObject);
            }
            else
            {
                uiImage.raycastTarget = true;
                transform.localPosition = Vector3.zero;
            } 
        }
        
    }
}
