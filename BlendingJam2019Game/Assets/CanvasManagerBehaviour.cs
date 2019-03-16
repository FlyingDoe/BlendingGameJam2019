﻿using System.Collections;
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

    // index de la premiere scene de jeu
    private int firstGameSceneBuildIndex;
    // index de la derniere scene de jeu
    private int lastGameSceneBuildIndex;

    // index de la scene de jeu actuelle
    private int currentGameSceneBuildIndex;

    // nextlevel button en public (un peu degueu mais va plus vite)
    public GameObject nextLevelButton;

    // Use this for initialization
    void Start()
    {
        instance = this;

        mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        winMenu = GameObject.FindGameObjectWithTag("WinMenu");

        // cherche les index des scenes de jeux
        bool first = true;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
        {
            string[] splitted = SceneUtility.GetScenePathByBuildIndex(i).Split('/');
            string sceneName = splitted[splitted.Length - 1];
            print(sceneName);

            if (sceneName.StartsWith("_main"))
            {
                if(first)
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

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayButtonClicked()
    {

        SceneManager.LoadScene(1);
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
}