using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManagerBehaviour : MonoBehaviour
{
    // instance (static moche)
    public static CanvasManagerBehaviour instance;

    // menu principal
    private GameObject mainMenu;

    // ecran pour montrer les commandes
    private GameObject controlMenu;

    // index de la premiere scene de jeu
    private int firstGameSceneBuildIndex;
    // index de la derniere scene de jeu
    private int lastGameSceneBuildIndex;

    // index de la scene de jeu actuelle
    private int currentGameSceneBuildIndex;

    [SerializeField] private Transform loadingScreenObj;

    public MusicManager musicMenu;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (instance)
        {
            Destroy(instance);
            instance = this;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
            controlMenu = GameObject.FindGameObjectWithTag("ControlMenu");

            controlMenu.SetActive(false);
            loadingScreenObj.gameObject.SetActive(false);
        }

    }

    // Use this for initialization
    void Start()
    {
        // cherche les index des scenes de jeux
        bool first = true;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
        {
            string[] splitted = SceneUtility.GetScenePathByBuildIndex(i).Split('/');
            string sceneName = splitted[splitted.Length - 1];
            print(sceneName);

            if (sceneName.StartsWith("_main"))
            {
                if (first)
                {
                    print("firstGameSceneBuildIndex = " + i);
                    firstGameSceneBuildIndex = i;
                    first = false;
                }
                lastGameSceneBuildIndex = i;
            }
        }

        // premiere scene jouee
        currentGameSceneBuildIndex = firstGameSceneBuildIndex;
    }


    public void OnPlayButtonClicked()
    {
        StartCoroutine(LoadLevel(1));
    }

    private IEnumerator LoadLevel(int i)
    {
        AsyncOperation async;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenu.SetActive(false);
            loadingScreenObj.gameObject.SetActive(true);
        }

        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        while (async.isDone == false)
        {
            //slider.value = async.progress;
            if (async.progress == 0.9f)
            {
                //slider.value = 1f;
                MusicManager.Instance.aS.clip = SfxManager.Instance.Music_stress;
                MusicManager.Instance.aS.Play();
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void OnQuitButtonQuit()
    {
        Application.Quit();
    }

    public void OnPlayerWin()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(2);
        MusicManager.Instance.aS.clip = SfxManager.Instance.Music_calm;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnPlayerFailed()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(3);
        MusicManager.Instance.aS.clip = SfxManager.Instance.Music_calm;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void BackToMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
        MusicManager.Instance.aS.clip = SfxManager.Instance.Music_calm;
        MusicManager.Instance.aS.Play();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnControlButtonClicked()
    {
        print("OnControlButtonClicked");
        controlMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OnBackToMenuClicked()
    {
        print("OnBackToMenuClicked");
        controlMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
