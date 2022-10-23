using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Checker : MonoBehaviour
{
    [SerializeField] private DropSelection[] orderedSelections;
    [SerializeField] private string[] actualAnswers;
    [SerializeField] private bool win = false;
    public GameObject winScreen;
    public AudioAsset incorrect;
    public AudioAsset winning;
    
    // Start is called before the first frame update
    void Start()
    {
        winScreen.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void OnClick()
    {
        //Debug.Log(IsCorrect());
        if (IsCorrect())
        {
            win = true;
            winScreen.SetActive(true);
            AudioManager.Instance.Play(winning);
        }
        else
        {
            AudioManager.Instance.Play(incorrect);
            win = false;
            winScreen.SetActive(false);
        }


    }

    public bool IsCorrect()
    {
        for (int i = 0; i < actualAnswers.Length; i++)
        {
            if (orderedSelections[i].GetSelection() != actualAnswers[i])
                return false;
        }

        return true;
    }
}
