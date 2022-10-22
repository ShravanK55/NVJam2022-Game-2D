using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;

    [SerializeField] private string[] characterStartNodes;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
