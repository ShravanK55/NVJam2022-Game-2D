using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    [SerializeField] private DropSelection[] orderedSelections;
    [SerializeField] private string[] actualAnswers;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(IsCorrect());
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
