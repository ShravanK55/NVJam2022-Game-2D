using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceNoise : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject choiceNoise;

    [SerializeField] private AudioAsset choiceNoiseSfx;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeNoise()
    {
        AudioManager.Instance.Play(choiceNoiseSfx);
    }
}
