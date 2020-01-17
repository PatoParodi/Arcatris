using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance
    {
        get;
        private set;
    }

    [SerializeField] AudioSource MusicMenu;
    [SerializeField] AudioSource MusicInGame;

    void Awake()
    {
        //First we check if there are any other instances conflicting
        if (Instance != null && Instance != this)
        {
            //Destroy other instances if they are not the same
            Destroy(gameObject);
        }
        //Save our current singleton instance
        Instance = this;
        //Make sure that the instance is not destroyed
        //between scenes (this is optional)
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusicInGame(){

        if(!MusicInGame.isPlaying)
            SoundManager.soundManager.playSound(MusicInGame);

    }

    public void PlayMusicMenu()
    {

        if (!MusicMenu.isPlaying)
            SoundManager.soundManager.playSound(MusicMenu);

    }

    public void StopMusicInGame()
    {

        MusicInGame.Stop();

    }

    public void StopMusicMenu()
    {

        MusicMenu.Stop();

    }

    public void PauseMusicInGame()
    {

        MusicInGame.Pause();

    }

    public void PauseMusicMenu()
    {

        MusicMenu.Pause();

    }
}
