using UnityEngine;

public static class GameManager
{
    public static bool isClear = false;
    public static bool isGameOver = false;
}

public class ResultScene : MonoBehaviour
{
    

    public AudioClip clearSE;
    public AudioClip gameOverSE;

    private AudioSource audioSource;
    private bool played = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (GameManager.isClear)
        {
           Play(clearSE);
        }
        else
        {
            Play(gameOverSE);
        }
    }

    void Play(AudioClip clip)
    {
        if (played) return;

        audioSource.PlayOneShot(clip);
        played = true;
    }
}