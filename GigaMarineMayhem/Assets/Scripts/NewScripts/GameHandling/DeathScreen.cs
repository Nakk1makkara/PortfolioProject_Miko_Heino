using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelsButtonClicked()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
