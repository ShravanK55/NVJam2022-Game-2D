using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSelection : MonoBehaviour, IDropHandler
{
    [SerializeField] private TabGroup tabGroup;
    private Image image;
    private string selection;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!tabGroup.selectedSprite) return;
        RectTransform panel = transform as RectTransform;
        if (RectTransformUtility.RectangleContainsScreenPoint(panel, Input.mousePosition))
        {
            image.sprite = tabGroup.selectedSprite;
            selection = tabGroup.selectedPersonName;
        }
    }

    public string GetSelection()
    {
        return selection;
    }
}