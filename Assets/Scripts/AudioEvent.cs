using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioEvent", menuName = "Audio/Audio Event")]
public class AudioEvent : ScriptableObject
{
    public AudioClip[] clips;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Header("Véletlenszerű Hangmagasság")]
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    
    public void Play(AudioSource source)
    {
        if (clips.Length == 0 || source == null) return;

        source.clip = clips[Random.Range(0, clips.Length)];
        source.volume = volume;
        source.pitch = Random.Range(minPitch, maxPitch);

        source.Play();
    }
}