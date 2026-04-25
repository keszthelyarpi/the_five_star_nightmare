using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Háttérzene")]
    public AudioSource bgmSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public void PlayBGM(AudioClip newMusic)
    {
        if (bgmSource.clip == newMusic) return; 

        bgmSource.Stop();
        bgmSource.clip = newMusic;
        bgmSource.loop = true;
        bgmSource.Play();
    }
}