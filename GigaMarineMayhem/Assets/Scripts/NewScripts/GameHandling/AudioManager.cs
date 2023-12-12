using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource levelCompleteMusic;
    public AudioSource DeathScreen;

    private void Start()
    {
        // Start playing the default background music
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        // Play or resume the regular background music
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
    }

    public void PlayLevelCompleteMusic()
    {
        // Stop the background music
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        // Play the level complete music
        if (levelCompleteMusic != null)
        {
            levelCompleteMusic.Play();
        }
    }

    public void PlayDeathMusic()
    {
        // Stop the background music
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        // Play the level complete music
        if (DeathScreen != null)
        {
            DeathScreen.Play();
        }
    }
}
