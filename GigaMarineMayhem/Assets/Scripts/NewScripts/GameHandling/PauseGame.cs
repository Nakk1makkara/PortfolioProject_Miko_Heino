using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;

    void Start()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;

            if (pauseMenu != null)
                pauseMenu.SetActive(false);
        }
        else
        {
            
            Time.timeScale = 0f;
           
            if (pauseMenu != null)
                pauseMenu.SetActive(true);
        }
    }

    
    public void ResumeGame()
    {
        TogglePause();
    }
}
