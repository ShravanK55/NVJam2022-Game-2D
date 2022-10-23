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

    private void Awake()
    {
        associatedSprite = GetComponent<Image>().sprite;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = originalBounds.Contains(Input.mousePosition) ? originalPosition : Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = originalPosition;
        tabGroup.selectedSprite = null;
        tabGroup.selectedPersonName = "";
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        var r = (RectTransform) transform;
        originalBounds = new Bounds(r.position, r.rect.size / 2);
        tabGroup.selectedSprite = associatedSprite;
        tabGroup.selectedPersonName = actualName;
    }
}
