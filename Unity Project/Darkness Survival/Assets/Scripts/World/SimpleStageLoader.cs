using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleStageLoader : MonoBehaviour
{
    public static SimpleStageLoader instance;

    [HideInInspector]
    public bool isStageLoading = false;

    [SerializeField] Animator transition;
    public float transitionTime = 1f;

    MusicManager musicManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    public void LoadMenuScene(string sceneName)
    {
        //if(GameManager.instance != null && GameManager.instance.isGamePaused)
        //{
        //    PauseManager.instance.UnPauseGame();
        //}

        musicManager.Play(musicManager.musicList[musicManager.musicList.Count - 1], false, transitionTime - 0.2f);
        StartCoroutine(LoadScene(sceneName));
    }
    public void LoadGameStagePC(string stageName)
    {
        musicManager.Play(musicManager.musicList[musicManager.musicList.Count - 1], false, transitionTime - 0.2f);
        StartCoroutine(LoadScene("EssentialPC", stageName));
    }

    public void LoadGameStageAndroid(string stageName)
    {
        musicManager.Play(musicManager.musicList[musicManager.musicList.Count - 1], false, transitionTime - 0.2f);
        StartCoroutine(LoadScene("EssentialAndroid", stageName));
    }

    IEnumerator LoadScene(string sceneName, string childSceneName = null)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transitionTime);

        if (childSceneName != null)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            SceneManager.LoadScene(childSceneName, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
