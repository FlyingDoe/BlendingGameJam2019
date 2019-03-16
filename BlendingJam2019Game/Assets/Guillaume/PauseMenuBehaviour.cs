using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
    // canvas
    private GameObject menu;

    // is the game pause or not
    private bool isPaused = false;

    // Use this for initialization
    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("Menu");
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                menu.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else
            {
                menu.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    public void OnResumeButtonClicked()
    {
        menu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OnRetryButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    public void OnQuitButtonCLicked()
    {

    }
}
