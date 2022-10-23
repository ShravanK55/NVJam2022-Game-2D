using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;

    [SerializeField] private Journal journalRef;

    [SerializeField] private string[] characterStartNodes;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        dialogueRunner.AddCommandHandler<string, string>("journal", journalRef.Add);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            dialogueRunner.StartDialogue(characterStartNodes[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            dialogueRunner.StartDialogue(characterStartNodes[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            dialogueRunner.StartDialogue(characterStartNodes[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            dialogueRunner.StartDialogue(characterStartNodes[3]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            dialogueRunner.StartDialogue(characterStartNodes[4]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            dialogueRunner.StartDialogue(characterStartNodes[5]);
        }
    }

    void Redraw(int currPage)
    {


    }
}
