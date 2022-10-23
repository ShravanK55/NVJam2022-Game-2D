using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public Button button;
    [SerializeField] private AudioAsset continueNoiseSfx;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(MakeNoise);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeNoise()
    {
        AudioManager.Instance.Play(continueNoiseSfx);
    }
}
