using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CreditsMenu;
    public GameObject CrossFadePanel;
    public string SceneName;

    public Animator crossFade;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(CrossFadePanel);
    }

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton();
    }

    IEnumerator LoadFirstScene()
    {
        CrossFadePanel.SetActive(true);
        crossFade.SetTrigger("Start"); // fade to black
        yield return new WaitForSeconds(1f); 
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += AfterLoad;
        void AfterLoad (UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            crossFade.SetTrigger("End"); // fade from black
            Destroy(CrossFadePanel);
            Destroy(this.gameObject);
        }
    }
    public void PlayNowButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called SceneName etc.)
        StartCoroutine(LoadFirstScene());
    }

    public void CreditsButton()
    {
        // Show Credits Menu
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void MainMenuButton()
    {
        // Show Main Menu
        MainMenu.SetActive(true);
        CreditsMenu.SetActive(false);
        CrossFadePanel.SetActive(false);
    }
}
