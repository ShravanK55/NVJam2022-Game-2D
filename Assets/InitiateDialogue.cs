using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateDialogue : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private string characterName;
    [SerializeField] private GameObject journal;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!dialogueManager)
        {
            dialogueManager = GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (journal && !journal.activeSelf && pauseMenu && !pauseMenu.activeSelf && winScreen && !winScreen.activeSelf)
        {
            dialogueManager.StartDialogue(characterName);
        }
    }
}
