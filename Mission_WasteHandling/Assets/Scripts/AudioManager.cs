using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music Library")]
    [SerializeField] private AudioEntry[] musicTracks;

    [Header("SFX Library")]
    [SerializeField] private AudioEntry[] sfxClips;

    private Dictionary<string, AudioClip> musicLookup;
    private Dictionary<string, AudioClip> sfxLookup;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        BuildLookups();

        AudioManager.Instance.PlayMusic("Menu");
    }

    private void BuildLookups()
    {
        musicLookup = new Dictionary<string, AudioClip>();
        sfxLookup = new Dictionary<string, AudioClip>();

        foreach (var entry in musicTracks)
        {
            if (!musicLookup.ContainsKey(entry.name))
                musicLookup.Add(entry.name, entry.clip);
        }

        foreach (var entry in sfxClips)
        {
            if (!sfxLookup.ContainsKey(entry.name))
                sfxLookup.Add(entry.name, entry.clip);
        }
    }

    // =================== API ===================

    public void PlayMusic(string name)
    {
        if (!musicLookup.TryGetValue(name, out AudioClip clip))
        {
            Debug.LogWarning($"Music '{name}' not found");
            return;
        }

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(string name, float volume = 1f)
    {
        if (!sfxLookup.TryGetValue(name, out AudioClip clip))
        {
            Debug.LogWarning($"SFX '{name}' not found");
            return;
        }

        sfxSource.PlayOneShot(clip, volume);
    }
}

[System.Serializable]
public class AudioEntry
{
    public string name;
    public AudioClip clip;
}