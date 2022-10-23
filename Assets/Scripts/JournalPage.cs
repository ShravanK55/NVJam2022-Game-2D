using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalPage : MonoBehaviour
{
    [Header("Person-specific variables")]
    public string associatedPerson;
    [SerializeField] private Sprite sprite;
    
    [Header("General journal page variables")]
    [SerializeField] private Image image;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI notesText;

    public void SetImage()
    {
        image.enabled = true;
        image.sprite = sprite;
        backgroundImage.color = new Color(0.75f, 0.75f, 0.75f);
    }

    public void OverwriteNotes(string notes)
    {
        notesText.text = notes;
    }
}
