using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    [SerializeField] private AudioAsset choiceNoiseSfx;
    [SerializeField] private AudioAsset talkingNoiseSfx;
    [SerializeField] private AudioAsset lindaNoiseSfx;
    [SerializeField] private AudioAsset johnNoiseSfx;
    [SerializeField] private AudioAsset steveNoiseSfx;
    [SerializeField] private AudioAsset ryanNoiseSfx;
    [SerializeField] private AudioAsset chrisNoiseSfx;
    [SerializeField] private AudioAsset ameliaNoiseSfx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeNoise(string character, string sfx)
    {
        if (character != "")
        {
            if (character == "Linda")
            {
                AudioManager.Instance.Play(lindaNoiseSfx);
            }
            else if (character == "John")
            {
                AudioManager.Instance.Play(johnNoiseSfx);
            }
            else if (character == "Steve")
            {
                AudioManager.Instance.Play(steveNoiseSfx);
            }
            else if (character == "Ryan")
            {
                AudioManager.Instance.Play(ryanNoiseSfx);
            }
            else if (character == "Chris")
            {
                AudioManager.Instance.Play(chrisNoiseSfx);
            }
            else if (character == "Amelia")
            {
                AudioManager.Instance.Play(ameliaNoiseSfx);
            }
        }
        else
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
}
