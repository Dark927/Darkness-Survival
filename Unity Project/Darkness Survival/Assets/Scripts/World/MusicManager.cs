using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> musicList;
    int musicIndex;

    AudioClip switchTo;
    AudioSource audioSource;

    float volume;
    [SerializeField] float timeToSwitch;

    StageTimer stageTimer;
    private float lastMusicChangeTime = 0;

    // Start 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stageTimer = FindObjectOfType<StageTimer>();
        Play(musicList[musicIndex], true);
        musicIndex = 0;
    }

    private void Update()
    {
        if (stageTimer != null)
        {

            if (stageTimer.time / 60f >= lastMusicChangeTime + 5)
            {
                musicIndex += 1;
                if (musicIndex >= musicList.Count)
                {
                    musicIndex = 0;
                }
                Play(musicList[musicIndex]);
                lastMusicChangeTime += 5;
            }
        }
    }

    // Play new music method 

    public void Play(AudioClip music, bool interrupt = false, float switchTime = 0f)
    {
        if (interrupt)
        {
            volume = 1f;
            audioSource.volume = volume;
            audioSource.clip = music;
            audioSource.Play();
        }
        else
        {
            switchTo = music;
            StartCoroutine(SmoothSwitchMusic(switchTime));
        }

    }

    // Coroutine for smooth switching

    IEnumerator SmoothSwitchMusic(float switchTime = 0f)
    {
        switchTime = switchTime == 0f ? timeToSwitch : switchTime;

        volume = 1f;

        while (volume > 0f)
        {
            volume -= Time.unscaledDeltaTime / switchTime;
            if (volume < 0f)
                volume = 0f;

            audioSource.volume = volume;

            yield return new WaitForEndOfFrame();
        }

        Play(switchTo, true);
    }
}
