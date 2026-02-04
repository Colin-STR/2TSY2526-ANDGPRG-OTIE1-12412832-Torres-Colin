using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip bgm;
    public AudioClip shootSFX;
    public AudioClip milestoneSFX;
    public AudioClip playerDeathSFX;
    public AudioClip gameOverSFX;

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
