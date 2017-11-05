
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{
    None,
    Teleport
}

public class SoundManager : MonoBehaviour {

    public static SoundManager main;

    [SerializeField]
    private List<GameSound> sounds = new List<GameSound>();

    private bool sfxMuted = false;

    [SerializeField]
    private bool musicMuted = false;
    public bool MusicMuted { get { return musicMuted; } }

    [SerializeField]
    private AudioSource musicSource;

    void Awake()
    {
        main = this;
    }

    private void Start()
    {
        if (musicMuted)
        {
            musicSource.Pause();
            //UIManager.main.ToggleMusic();
        } else
        {
            musicSource.Play();
        }
    }

    public void PlaySound(SoundType soundType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.soundType == soundType)
                {
                    gameSound.sound.Play();
                }
            }
        }
    }

    public void PlayActionSound(Action actionType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.actionType == actionType)
                {
                    gameSound.sound.Play();
                }
            }
        }
    }

    public void ToggleSfx()
    {
        sfxMuted = !sfxMuted;
        //UIManager.main.ToggleSfx();
    }

    public bool ToggleMusic()
    {
        musicMuted = !musicMuted;
        if (musicMuted)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.Play();
        }
        //UIManager.main.ToggleMusic();
        return musicMuted;
    }
}

[System.Serializable]
public class GameSound : System.Object
{
    public SoundType soundType;
    public Action actionType;
    public AudioSource sound;
}
