using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Journal : MonoBehaviour
{
    [SerializeField] private GameObject journal;

    private Dictionary<string, List<string>> myDict = new Dictionary<string, List<string>>();

    [SerializeField] private JournalPage[] pages;
    
    [SerializeField] private AudioAsset journalOpenSfx;
    [SerializeField] private AudioAsset journalCloseSfx;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Add(string key, string value)
    {
        if (myDict.ContainsKey(key) == false)
        {
            List<string> addList = new List<string>();
            myDict.Add(key, addList);
        }
        myDict[key].Add(value);
        string result = "";
        for (int i = 0; i < myDict[key].Count; i++)
        {
            result += myDict[key][i];
        }
        foreach (var note in pages)
        {
            if (note.associatedPerson == key)
            {
                note.OverwriteNotes(result);
                break;
            }
        }
    }

    // called when journal button is clicked
    public void OnClick()
    {
        if (journal.activeSelf)
        {
            AudioManager.Instance.Play(journalCloseSfx);
        }
        else
        {
            AudioManager.Instance.Play(journalOpenSfx);
        }
        journal.SetActive(!journal.activeSelf);
    }

    public void DiscoverPage(string name)
    {
        foreach (var note in pages)
        {
            if (note.associatedPerson == name)
            {
                note.SetImage();
            }
        }
    }
}
