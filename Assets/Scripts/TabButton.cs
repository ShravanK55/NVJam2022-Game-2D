using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public TabGroup tabGroup;

    public Image background;

    public int index;

    public Sprite associatedSprite;
    private Vector3 originalPosition;
    private Bounds originalBounds;

    private bool dragging;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!dragging || originalBounds.Contains(Input.mousePosition))
            tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        originalPosition = transform.position;
        var r = (RectTransform) transform;
        originalBounds = new Bounds(r.position, r.rect.size / 2);
        if (tabGroup.displaying) return;
        tabGroup.selectedSprite = associatedSprite;
    }
}
