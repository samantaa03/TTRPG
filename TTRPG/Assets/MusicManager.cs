using UnityEngine;

public class MusicManager : MonoBehaviour {
    public static MusicManager Instance;

    [SerializeField] private AudioSource audioSource;

    [Header("Assign Background Music")]
    public AudioClip adventure;  // Peaceful music
    public AudioClip happy;  // Suspense music
    public AudioClip suspense;    // Action music
    public AudioClip drama;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
        }
    }

    public void PlayMusic(MusicChoice choice) {
        AudioClip clip = GetMusic(choice);
        if (clip != null && audioSource.clip != clip) {
            audioSource.clip = clip;
            audioSource.Play();
        } else if (clip == null) {
            audioSource.Stop();
        }
    }

    public AudioClip GetMusic(MusicChoice choice) {
        switch (choice) {
            case MusicChoice.adventure: return adventure;
            case MusicChoice.happy: return happy;
            case MusicChoice.suspense: return suspense;
            case MusicChoice.drama: return drama;
            default: return null;
        }
    }
    private void Start() {
    PlayMusic(MusicChoice.adventure);  // Change this to test different tracks
}
}
