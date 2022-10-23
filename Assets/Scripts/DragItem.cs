using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public TabGroup tabGroup;
    public string actualName;
    private Sprite associatedSprite;
    private Vector3 originalPosition;
    private Bounds originalBounds;

    private bool dragging;

    private void Awake()
    {
        associatedSprite = GetComponent<Image>().sprite;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (tabGroup.displaying) return;
        transform.position = originalBounds.Contains(Input.mousePosition) ? originalPosition : Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        if (tabGroup.displaying) return;
        transform.position = originalPosition;
        tabGroup.selectedSprite = null;
        tabGroup.selectedPersonName = "";
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        originalPosition = transform.position;
        var r = (RectTransform) transform;
        originalBounds = new Bounds(r.position, r.rect.size / 2);
        if (tabGroup.displaying) return;
        tabGroup.selectedSprite = associatedSprite;
        tabGroup.selectedPersonName = actualName;
    }
}
