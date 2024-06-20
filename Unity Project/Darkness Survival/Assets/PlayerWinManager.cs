using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinManager : MonoBehaviour
{
    [SerializeField] GameObject winMessagePanel;
    [SerializeField] DataContainer dataContainer;

    public void Win()
    {
        PauseManager.instance.PauseGame();
        winMessagePanel.SetActive(true);
        dataContainer.StageComplete(0);
    }
}
