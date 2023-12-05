using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    public Image progressBarImage;
    public TextMeshProUGUI progressText;
    public GameObject levelCompletePanel;

    [SerializeField] private int totalGoals = 10;
    [SerializeField] private int goalsAchieved = 0;

    private bool isLevelComplete = false;
    private AudioManager audioManager;

    private void Start()
    {
        if (progressBarImage == null || progressText == null || levelCompletePanel == null)
        {
            Debug.LogError("Progress bar image, TMP text, or Level Completed panel not assigned in the inspector!");
        }

        // Find and store the AudioManager in the scene
        audioManager = FindObjectOfType<AudioManager>();

        if (audioManager == null)
        {
            Debug.LogError("AudioManager script not found in the scene.");
        }

        InitializeProgressBar();
    }

    private void InitializeProgressBar()
    {
        UpdateProgressBar();
    }

    public void GoalAchieved()
    {
        if (!isLevelComplete)
        {
            goalsAchieved++;
            UpdateProgressBar();

            if (goalsAchieved >= totalGoals)
            {
                LevelComplete();
            }
        }
    }

    public bool IsLevelComplete()
    {
        return isLevelComplete;
    }

    private void UpdateProgressBar()
    {
        float progress = (float)goalsAchieved / totalGoals;

        if (progressBarImage != null)
        {
            progressBarImage.transform.localScale = new Vector3(progress, 1f, 1f);
        }

        if (progressText != null)
        {
            progressText.text = (progress * 100f).ToString("F0") + "%";
        }
    }

    private void LevelComplete()
    {
        isLevelComplete = true;

        // Show the Level Completed panel
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
        }

        // Implement logic for when the level is complete
        Debug.Log("Level complete!");

        // Notify the AudioManager to play level complete music
        if (audioManager != null)
        {
            audioManager.PlayLevelCompleteMusic();
        }
    }

    // Allow setting the total goals in the Unity Editor
    #if UNITY_EDITOR
    void OnValidate()
    {
        if (totalGoals < 1)
            totalGoals = 1;
    }
    #endif
}
