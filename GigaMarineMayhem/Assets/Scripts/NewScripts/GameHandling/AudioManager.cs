using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource levelCompleteMusic;
    public AudioSource DeathScreen;

    private void Start()
    {
        
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
    }

    public void PlayLevelCompleteMusic()
    {
        
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        
        if (levelCompleteMusic != null)
        {
            levelCompleteMusic.Play();
        }
    }

    public void PlayDeathMusic()
    {
        
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        
        if (DeathScreen != null)
        {
            DeathScreen.Play();
        }
    }
}
