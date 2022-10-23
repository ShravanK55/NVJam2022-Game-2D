using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;

    [SerializeField] private Journal journalRef;

    [SerializeField] private string[] characterStartNodes;

    [SerializeField] private NameSpriteMapping[] spriteMappings;
    private Dictionary<string, Sprite> characterSprites;
    [SerializeField] private Image speakingCharacter;

    [Serializable]
    private class NameSpriteMapping
    {
        public string name;
        public Sprite person;

        public NameSpriteMapping(string name, Sprite person)
        {
            this.name = name;
            this.person = person;
        }
    }

    void Awake()
    {
        dialogueRunner.AddCommandHandler<string, string>("journal", journalRef.Add);
        dialogueRunner.AddCommandHandler<string>("setSpeaker", SetSpeaker);

        characterSprites = new Dictionary<string, Sprite>();
        foreach (var kp in spriteMappings)
        {
            characterSprites[kp.name] = kp.person;
        }
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

    public void SetSpeaker(string name)
    {
        speakingCharacter.sprite = characterSprites[name];
    }
}
