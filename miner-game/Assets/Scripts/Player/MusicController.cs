using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioClip mainMusic;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameOverMusic;

    [SerializeField] AudioSource musicPlayer;

    public void PutMainMusic()
    {
        musicPlayer.clip = mainMusic;
        musicPlayer.Play();
    }
    public void PutMenuMusic()
    {
        musicPlayer.clip = menuMusic;
        musicPlayer.Play();
    }
    public void PutGameOverMusic()
    {
        musicPlayer.loop = false;
        musicPlayer.clip = gameOverMusic;
        musicPlayer.Play();
    }
}
