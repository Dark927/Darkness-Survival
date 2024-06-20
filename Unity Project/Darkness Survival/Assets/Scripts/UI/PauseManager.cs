using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    private void Start()
    {
        instance = this;
        UnPauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameManager.instance.isGamePaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        GameManager.instance.isGamePaused = false;
    }
}
