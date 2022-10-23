using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;
using static Yarn.Unity.DialogueRunner;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;

    [SerializeField] private Journal journalRef;
    [SerializeField] private Journal noiseRef;

    [SerializeField] private string[] characterStartNodes;

    [SerializeField] private GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner.onDialogueComplete.AddListener(ReactivateTab); // The onDialogueComplete unityevnt will be invoked when a dialogue ends.
    }

    void Awake()
    {
        dialogueRunner.AddCommandHandler<string, string>("journal", journalRef.Add);
        dialogueRunner.AddCommandHandler<string, string>("makeNoise", noiseRef.MakeNoise);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            button.SetActive(false); // disable tab when dialogue starts
            dialogueRunner.StartDialogue(characterStartNodes[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            button.SetActive(false); // disable tab when dialogue starts
            dialogueRunner.StartDialogue(characterStartNodes[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            button.SetActive(false); // disable tab when dialogue starts
            dialogueRunner.StartDialogue(characterStartNodes[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            button.SetActive(false); // disable tab when dialogue starts
            dialogueRunner.StartDialogue(characterStartNodes[3]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            button.SetActive(false); // disable tab when dialogue starts
            dialogueRunner.StartDialogue(characterStartNodes[4]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            button.SetActive(false); // disable tab when dialogue starts
            dialogueRunner.StartDialogue(characterStartNodes[5]);
        }
    }

    void Redraw(int currPage)
    {


    }

    void ReactivateTab()
    {
        button.SetActive(true);
    }
}
