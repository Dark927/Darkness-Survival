using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;

    private void Start()
    {
        PauseManager.instance.UnPauseGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !SimpleStageLoader.instance.isStageLoading)
        {
            if(!panel.activeInHierarchy)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        } 
    }

    public void CloseMenu()
    {
        PauseManager.instance.UnPauseGame();
        panel.SetActive(false);
    }

    public void OpenMenu()
    {
        PauseManager.instance.PauseGame();
        panel.SetActive(true);
    }
}        
