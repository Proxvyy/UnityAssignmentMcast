using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private float volume = 0.35f;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        AudioSource src = GetComponent<AudioSource>();
        src.clip = backgroundMusic;
        src.loop = true;
        src.volume = volume;
        src.spatialBlend = 0f;

        if (!src.isPlaying)
        {
            src.Play();
        }
    }
}
