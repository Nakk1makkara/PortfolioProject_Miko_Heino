using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void Level2()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void Level3()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void Level4()
    {
        SceneManager.LoadSceneAsync(5);
    }
}
