using UnityEngine;
using System.Collections;

public class StaticSoundEffect : MonoBehaviour
{
    [Header("Audio Clip")]
    public AudioClip staticSound;

    [Header("Volume")]
    [Range(0f, 1f)]
    public float volume = 0.12f;

    [Header("Timing")]
    public float startDelay = 2f;
    public float playDuration = 1f;
    public float repeatDelay = 4f;

    [Header("Mode")]
    public bool continuousLoop = false; // TRUE = nonstop loop

    private AudioSource audioSource;
    private Coroutine playRoutine;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = staticSound;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = volume;
    }

    void Start()
    {
        StartPlayback();
    }

    // ---------- PUBLIC BUTTON FUNCTIONS ----------

    public void StartPlayback()
    {
        StopPlayback();

        playRoutine = StartCoroutine(PlaybackRoutine());
    }

    public void StopPlayback()
    {
        if (playRoutine != null)
        {
            StopCoroutine(playRoutine);
            playRoutine = null;
        }

        audioSource.Stop();
    }

    public void TogglePlayback()
    {
        if (audioSource.isPlaying)
            StopPlayback();
        else
            StartPlayback();
    }

    // ---------- MAIN ROUTINE ----------

    IEnumerator PlaybackRoutine()
    {
        yield return new WaitForSeconds(startDelay);

        if (continuousLoop)
        {
            audioSource.loop = true;
            audioSource.Play();
            yield break;
        }

        while (true)
        {
            audioSource.loop = false;
            audioSource.Play();

            yield return new WaitForSeconds(playDuration);

            audioSource.Stop();

            yield return new WaitForSeconds(repeatDelay);
        }
    }
}
