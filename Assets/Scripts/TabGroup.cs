using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public TabButton selectedTab;
    public List<RectTransform> objectsToSwap;
    public RectTransform guestList;
    public RectTransform row1Buttons;
    public RectTransform row2Buttons;
    private bool displaying;
    [SerializeField] private AudioAsset journalTabSwitchSfx;

    [HideInInspector] public Sprite selectedSprite;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || selectedTab != button)
        {
            button.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.sprite = tabActive;
        int index = button.index;
        DisplayTab(index);
    }
    
    private void DisplayTab(int index)
    {
        AudioManager.Instance.Play(journalTabSwitchSfx);
        if (!displaying && index >= 0 && index < objectsToSwap.Count)
        {
            var row1Midway = new Vector2(0, 300);
            var row1End = new Vector2(265, 300);
            var row2Midway = new Vector2(0, 0);
            var row2End = new Vector2(400, 0);
            float guestListEnd = 1280;
            DOTween.To(() => row1Buttons.anchoredPosition,
                    pos => row1Buttons.anchoredPosition = pos,
                    row1Midway, 0.15f)
                .SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    DOTween.To(() => row1Buttons.anchoredPosition,
                            pos => row1Buttons.anchoredPosition = pos,
                            row1End, 0.1f)
                        .SetEase(Ease.OutExpo);
                });
            DOTween.To(() => row2Buttons.anchoredPosition,
                    pos => row2Buttons.anchoredPosition = pos,
                    row2Midway, 0.15f)
                .SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    DOTween.To(() => row2Buttons.anchoredPosition,
                            pos => row2Buttons.anchoredPosition = pos,
                            row2End, 0.1f)
                        .SetEase(Ease.OutExpo);
                });
            DOTween.To(() => guestList.anchoredPosition.x,
                    x => guestList.anchoredPosition = new Vector2(x, guestList.anchoredPosition.y),
                    guestListEnd, 0.25f)
                .SetEase(Ease.OutQuart);
            displaying = true;
        }
        for (int i = 0; i < objectsToSwap.Count; ++i)
        {
            if (i == index)
            {
                var i1 = i;
                objectsToSwap[i1].gameObject.SetActive(true);
                DOTween.To(() => objectsToSwap[i1].anchoredPosition.y,
                        y => objectsToSwap[i1].anchoredPosition = new Vector2(objectsToSwap[i1].anchoredPosition.x, y),
                        0, 0.5f)
                    .SetEase(Ease.OutBack);
            }
            else
            {
                var i1 = i;
                DOTween.To(() => objectsToSwap[i1].anchoredPosition.y,
                        y => objectsToSwap[i1].anchoredPosition = new Vector2(objectsToSwap[i1].anchoredPosition.x, y),
                        -600, 0.2f)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        objectsToSwap[i1].gameObject.SetActive(false);                        
                    });
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && selectedTab == button) { continue; }
            button.background.sprite = tabIdle;
        }
    }

    public void ResetPositions()
    {
        DisplayTab(-1);
        displaying = false;
        var row1Midway = new Vector2(0, 300);
        var row1End = new Vector2(0, 160);
        var row2Midway = new Vector2(0, 0);
        var row2End = new Vector2(0, -160);
        float guestListEnd = 438;
        DOTween.To(() => row1Buttons.anchoredPosition,
                pos => row1Buttons.anchoredPosition = pos,
                row1Midway, 0.15f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                DOTween.To(() => row1Buttons.anchoredPosition,
                        pos => row1Buttons.anchoredPosition = pos,
                        row1End, 0.1f)
                    .SetEase(Ease.OutExpo);
            });
        DOTween.To(() => row2Buttons.anchoredPosition,
                pos => row2Buttons.anchoredPosition = pos,
                row2Midway, 0.15f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                DOTween.To(() => row2Buttons.anchoredPosition,
                        pos => row2Buttons.anchoredPosition = pos,
                        row2End, 0.1f)
                    .SetEase(Ease.OutExpo);
            });
        DOTween.To(() => guestList.anchoredPosition.x,
                x => guestList.anchoredPosition = new Vector2(x, guestList.anchoredPosition.y),
                guestListEnd, 0.25f)
            .SetEase(Ease.OutQuad);
    }
}
