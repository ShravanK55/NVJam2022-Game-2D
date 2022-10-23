using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{
    [SerializeField] private GameObject journal;

    private Dictionary<string, HashSet<string>> myDict = new Dictionary<string, HashSet<string>>();

    [SerializeField] private JournalPage[] pages;
    
    [SerializeField] private AudioAsset journalOpenSfx;
    [SerializeField] private AudioAsset journalCloseSfx;
    [SerializeField] private AudioAsset journalUpdateSfx;
    [SerializeField] private AudioAsset choiceNoiseSfx;
    [SerializeField] private AudioAsset talkingNoiseSfx;


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
            HashSet<string> addList = new HashSet<string>();
            myDict.Add(key, addList);
        }
        AudioManager.Instance.Play(journalUpdateSfx); // plays when a new value gets appended.
        myDict[key].Add(value);
        string result = "";
        foreach (var s in myDict[key])
        {
            result += s + "\n";
        }
        result = result.Substring(0, result.Length - 1);
        
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

    public void HideIfNeeded(string arg0)
    {
        if (journal.activeSelf)
        {
            AudioManager.Instance.Play(journalCloseSfx);
            journal.SetActive(false);
        }
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

    public void MakeNoise(string a, string sfx)
    {
        if (sfx == "choiceNoise")
        {
            AudioManager.Instance.Play(choiceNoiseSfx);
        }
        else if (sfx == "talkingNoise")
        {
            AudioManager.Instance.Play(talkingNoiseSfx);
        }
    }
}
