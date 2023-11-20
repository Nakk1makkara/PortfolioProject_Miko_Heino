using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsScript : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
