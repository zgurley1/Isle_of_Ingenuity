using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using System;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        Deselect();
    }

    public void Select() {
        image.color = selectedColor;
    }
    
    public void Deselect() {
        image.color = notSelectedColor;
    }    public void OnDrop(PointerEventData eventData) {
        if (transform.childCount == 0) {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }
}
