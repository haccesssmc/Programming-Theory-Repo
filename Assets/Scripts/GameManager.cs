using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    public bool isGameActive { get; private set; }

    private void Start()
    {
        isGameActive = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseHandler();
        }
    }

    void PauseHandler()
    {
        if (pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            isGameActive = true;
        }
        else
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            isGameActive = false;
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
