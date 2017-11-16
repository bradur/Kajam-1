
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

    [SerializeField]
    private AudioSource introSource;


    private List<GameSound> lerpPitchSounds = new List<GameSound>();

    private float maxPitch = 2.5f;
    private float pitchLerpSpeedUp = 0.2f;
    private float minPitch = 2f;
    private float pitchLerpSpeedDown = 0.1f;

    private bool isIntro = true;

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
                        gameSound.sound.pitch = 1f;
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
        if (!musicMuted)
        {
            introSource.Play();
        }
    }

    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public void FadeOutSound(SoundType soundType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.soundType == soundType)
                {
                    StartCoroutine(FadeOut(gameSound.sound, 1.5f));
                }
            }
        }
    }


    public void PlaySoundAndLoopIt(SoundType soundType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.soundType == soundType)
                {
                    gameSound.sound.loop = true;
                    gameSound.sound.Play();
                }
            }
        }
    }

    public void StopLoopingSound(SoundType soundType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.soundType == soundType)
                {
                    gameSound.sound.loop = false;
                }
            }
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


    IEnumerator DelayedLoopStop(AudioSource sound, float time)
    {
        yield return new WaitForSeconds(time);
        sound.loop = false;
    }

    public void StopLoopingSoundWithDelay(SoundType soundType, float delay)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.soundType == soundType)
                {
                    StartCoroutine(DelayedLoopStop(gameSound.sound, delay));
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

    IEnumerator DelayedStart(AudioSource sound, float time)
    {
        yield return new WaitForSeconds(time);
        sound.Play();
    }


    public void StartMusic()
    {
        isIntro = false;
        if (!musicMuted)
        {
            StartCoroutine(FadeOut(introSource, 0.2f));
            musicSource.Play();
        }
    }

    public bool ToggleMusic()
    {
        musicMuted = !musicMuted;
        if (musicMuted)
        {
            if (isIntro)
            {
                introSource.Pause();
            } else
            {
                musicSource.Pause();
            }
        }
        else
        {
            if (isIntro)
            {
                introSource.Play();
            }
            else
            {
                musicSource.Play();
            }
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
