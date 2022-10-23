using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using static Yarn.Unity.DialogueRunner;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;

    [SerializeField] private GameObject button;

    [SerializeField] private Journal journalRef;
    [SerializeField] private Journal noiseRef;

    [SerializeField] private string[] characterStartNodes;

    [SerializeField] private NameSpriteMapping[] spriteMappings;
    private Dictionary<string, Sprite> characterSprites;
    [SerializeField] private Image speakingCharacter;

    [SerializeField] private AudioAsset crowdSfx;
    private int crowdSfxChannel;

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
        dialogueRunner.onNodeStart.AddListener(journalRef.HideIfNeeded);
        dialogueRunner.onNodeStart.AddListener(FocusOnSpeaker);
        dialogueRunner.onDialogueComplete.AddListener(UnfocusOffSpeaker);
        dialogueRunner.onDialogueComplete.AddListener(ReactivateTab); // The onDialogueComplete unityevnt will be invoked when a dialogue ends.

        dialogueRunner.AddCommandHandler<string, string>("journal", journalRef.Add);
        dialogueRunner.AddCommandHandler<string, string>("makeNoise", noiseRef.MakeNoise);
        dialogueRunner.AddCommandHandler<string>("setSpeaker", SetSpeaker);

        characterSprites = new Dictionary<string, Sprite>();
        foreach (var kp in spriteMappings)
        {
            characterSprites[kp.name] = kp.person;
        }
    }

    private void Start()
    {
        crowdSfxChannel = AudioManager.Instance.Play(crowdSfx);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartDialogue(characterStartNodes[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartDialogue(characterStartNodes[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartDialogue(characterStartNodes[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartDialogue(characterStartNodes[3]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StartDialogue(characterStartNodes[4]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            StartDialogue(characterStartNodes[5]);
        }
    }

    public void StartDialogue(string characterName)
    {
        button.SetActive(false); // disable tab when dialogue starts
        dialogueRunner.StartDialogue(characterName);
    }

    public void SetSpeaker(string name)
    {
        speakingCharacter.sprite = characterSprites[name];
    }

    void ReactivateTab()
    {
        button.SetActive(true);
    }

    private void FocusOnSpeaker(string arg0)
    {
        StartCoroutine(FadeCrowdSfx(1, 0.25f, 1.5f));
    }

    private void UnfocusOffSpeaker()
    {
        StartCoroutine(FadeCrowdSfx(0.25f, 1f, 1.5f));
    }

    private IEnumerator FadeCrowdSfx(float start, float end, float time)
    {
        float timer = 0f;
        while (timer < time)
        {
            timer += Time.deltaTime;
            var vol = Mathf.Lerp(start, end, timer / time);
            AudioManager.Instance.SetVolume("AmbienceVolume", vol);
            yield return null;
        }
        AudioManager.Instance.SetVolume("AmbienceVolume", end);
    }
}
