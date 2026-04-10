using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip bgm;
    public AudioClip cannonSFX;
    public AudioClip crossbowSFX;
    public AudioClip gameOverSFX;
    public AudioClip monsterDeathSFX;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        PlayMusic(bgm);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
