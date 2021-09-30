using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    // ENCAPSULATION
    // prevented changing the variable from other classes 
    public bool isGameActive { get; private set; } = true;


    void Update()
    {
        PauseHandler();
    }


    void PauseHandler()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

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
