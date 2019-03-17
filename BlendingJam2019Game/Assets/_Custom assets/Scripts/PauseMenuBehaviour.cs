using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
    // canvas
    [SerializeField] private RectTransform menu;

    // is the game pause or not
    private bool isPaused = false;

    // Use this for initialization
    void Start()
    {
        menu.gameObject.SetActive(isPaused);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                //CanvasManagerBehaviour.instance.UnlockAndShowCursor();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                menu.gameObject.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                menu.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    public void OnResumeButtonClicked()
    {
        menu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        //CanvasManagerBehaviour.instance.LockAndHideCursor();
    }

    public void OnRetryButtonClicked()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        menu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        // CanvasManagerBehaviour.instance.LockAndHideCursor();
        CanvasManagerBehaviour.instance.OnPlayButtonClicked();
    }

    public void OnQuitButtonCLicked()
    {
        CanvasManagerBehaviour.instance.OnQuitButtonQuit();
    }
}
