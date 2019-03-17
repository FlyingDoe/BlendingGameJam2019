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

    // ecran si Win
    private GameObject winMenu;

    // ecran si fail
    private GameObject failMenu;

    // ecran pour montrer les commandes
    private GameObject controlMenu;

    // index de la premiere scene de jeu
    private int firstGameSceneBuildIndex;
    // index de la derniere scene de jeu
    private int lastGameSceneBuildIndex;

    // index de la scene de jeu actuelle
    private int currentGameSceneBuildIndex;

    [SerializeField] private Transform loadingScreenObj;

    // nextlevel button en public (un peu degueu mais va plus vite)
    public GameObject nextLevelButton;

    private void Awake()
    {
        instance = this;

        mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        winMenu = GameObject.FindGameObjectWithTag("WinMenu");
        failMenu = GameObject.FindGameObjectWithTag("FailMenu");
        controlMenu = GameObject.FindGameObjectWithTag("ControlMenu");

        winMenu.SetActive(false);
        failMenu.SetActive(false);
        controlMenu.SetActive(false);
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

        mainMenu.SetActive(false);
        loadingScreenObj.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        async = SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
        async.allowSceneActivation = false;
        while (async.isDone == false)
        {
            //slider.value = async.progress;
            if (async.progress == 0.9f)
            {
                //slider.value = 1f;
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
        // decharger la scene de jeu puis afficher l'ecran de Win !!!
        SceneManager.UnloadSceneAsync(currentGameSceneBuildIndex);
        winMenu.SetActive(true);

        // s'il y a un niveau suivant => bouton NextLevel
        if (currentGameSceneBuildIndex == lastGameSceneBuildIndex)
            nextLevelButton.SetActive(false);
        else
        {
            nextLevelButton.SetActive(true);
            ++currentGameSceneBuildIndex;
        }
    }

    public void OnPlayerFailed()
    {
        // decharger la scene de jeu puis afficher l'ecran de Win !!!
        SceneManager.UnloadSceneAsync(currentGameSceneBuildIndex);
        failMenu.SetActive(true);
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

    public void LockAndHideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockAndShowCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
