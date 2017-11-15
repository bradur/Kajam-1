
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{
    None,
    Teleport,
    Charge,
    Shoot,
    BeingPulled,
    ProjectileHitWall
}

public class SoundManager : MonoBehaviour
{

    public static SoundManager main;

    [SerializeField]
    private List<GameSound> sounds = new List<GameSound>();

    private bool sfxMuted = false;

    [SerializeField]
    private bool musicMuted = false;
    public bool MusicMuted { get { return musicMuted; } }

    [SerializeField]
    private AudioSource musicSource;

    private List<GameSound> lerpPitchSounds = new List<GameSound>();

    private float maxPitch = 2.5f;
    private float pitchLerpSpeedUp = 0.2f;
    private float minPitch = 1f;
    private float pitchLerpSpeedDown = 0.1f;

    void Awake()
    {
        main = this;
    }

    private void Update()
    {
        for (int i = 0; i < lerpPitchSounds.Count; i += 1)
        {
            GameSound gameSound = lerpPitchSounds[i];
            if (gameSound.sound.isPlaying)
            {
                if (gameSound.lerpingUp)
                {
                    gameSound.lerpTimer += Time.unscaledDeltaTime * pitchLerpSpeedUp;
                    gameSound.sound.pitch = Mathf.Lerp(gameSound.sound.pitch, maxPitch, gameSound.lerpTimer);
                    if (Mathf.Abs(gameSound.sound.pitch - maxPitch) < 0.01f)
                    {
                        gameSound.lerpingUp = false;
                        gameSound.sound.pitch = maxPitch;
                    }
                }
                else if (gameSound.lerpingDown)
                {
                    gameSound.lerpTimer += Time.unscaledDeltaTime * pitchLerpSpeedDown;
                    gameSound.sound.pitch = Mathf.Lerp(gameSound.sound.pitch, minPitch, gameSound.lerpTimer);
                    if (Mathf.Abs(gameSound.sound.pitch - minPitch) < 0.01f)
                    {
                        gameSound.lerpingDown = false;
                        gameSound.sound.pitch = minPitch;
                    }
                }
                else
                {
                    gameSound.lerpTimer = 0f;
                    lerpPitchSounds.Remove(gameSound);
                }
            }
            else
            {
                gameSound.lerpTimer = 0f;
                lerpPitchSounds.Remove(gameSound);
            }
        }
    }

    private void Start()
    {
        if (musicMuted)
        {
            musicSource.Pause();
            //UIManager.main.ToggleMusic();
        }
        else
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
                    if (gameSound.sound.isPlaying)
                    {
                        gameSound.sound.Stop();
                    }
                    gameSound.sound.Play();
                }
            }
        }
    }

    IEnumerator DelayedStop(AudioSource sound, float time)
    {
        yield return new WaitForSeconds(time);
        sound.Stop();
    }

    public void StopSoundWithDelay(SoundType soundType, float delay)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.soundType == soundType)
                {
                    StartCoroutine(DelayedStop(gameSound.sound, delay));
                }
            }
        }
    }

    public void LerpPitchUp(SoundType soundType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.soundType == soundType)
                {
                    gameSound.lerpingUp = true;
                    lerpPitchSounds.Add(gameSound);
                }
            }
        }
    }

    public void LerpPitchDown(SoundType soundType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.soundType == soundType)
                {
                    gameSound.lerpingDown = true;
                    lerpPitchSounds.Add(gameSound);
                }
            }
        }
    }

    public void PlaySoundIfNotPlaying(SoundType soundType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.soundType == soundType && !gameSound.sound.isPlaying)
                {
                    gameSound.sound.Play();
                }
            }
        }
    }

    public void PlayRandomSound(SoundType soundType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.soundType == soundType)
                {
                    gameSound.sounds[Random.Range(0, gameSound.sounds.Count - 1)].Play();
                }
            }
        }
    }

    public void StopSound(SoundType soundType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.soundType == soundType && gameSound.sound.isPlaying)
                {
                    gameSound.sound.Stop();
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
    public List<AudioSource> sounds;
    public float lerpTimer = 0f;
    public bool lerpingUp;
    public bool lerpingDown;
}
