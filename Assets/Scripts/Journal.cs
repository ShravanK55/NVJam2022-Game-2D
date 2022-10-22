using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{
    [SerializeField] private GameObject journal;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("hi");
    }

    // called when journal button is clicked
    public void OnClick()
    {
        journal.SetActive(!journal.activeSelf);

    }
}
