using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform playerTransform;

    public bool isGamePaused = false;
    [SerializeField] SimpleStageLoader levelLoader;

    [SerializeField] GameObject AndroidUI;
    public bool isAndroid = false;


    private void Awake()
    {
        instance = this;
        levelLoader.gameObject.SetActive(true);

        if (isAndroid)
        {
            AndroidUI.SetActive(true);
        }
        else
        {
            AndroidUI.SetActive(false);
        }
    }


}
