using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField] GameObject weaponsParent;

    public void GameOver()
    {
        Debug.Log("Game over");
        GetComponent<PlayerMove>().enabled = false;

        gameOverPanel.SetActive(true);
        GrayscaleEffect.instance.ToggleGrayscale(true);
        weaponsParent.SetActive(false);
    }
}