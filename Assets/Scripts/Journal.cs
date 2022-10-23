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

    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private TextMeshProUGUI text3;
    [SerializeField] private TextMeshProUGUI text4;
    [SerializeField] private TextMeshProUGUI text5;
    [SerializeField] private TextMeshProUGUI text6;
    
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
            List<string> addList = new List<string>();
            myDict.Add(key, addList);
        }
        AudioManager.Instance.Play(journalUpdateSfx);
        myDict[key].Add(value);
        string result = "";
        for (int i = 0; i < myDict[key].Count; i++)
        {
            result += myDict[key][i];
        }
        if (key == "Amelia")
        {
            text1.text = result;
        } else if (key == "Chris")
        {
            text2.text = result;
        } else if (key == "Linda")
        {
            text3.text = result;
        } else if (key == "Ryan")
        {
            text4.text = result;
        } else if (key == "John")
        {
            text5.text = result;
        } else if (key == "Steve")
        {
            text6.text = result;
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

    public void Redraw(int input)
    {
    
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
