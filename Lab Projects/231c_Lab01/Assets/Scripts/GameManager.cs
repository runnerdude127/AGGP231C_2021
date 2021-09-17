using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject winText;
    public GameObject loseText;
    Scene scene;

    private void Awake()
    {
        instance = this;
        scene = SceneManager.GetActiveScene();
    }

    public void winCondition()
    {
        winText.SetActive(true);
    }

    public void loseCondition()
    {
        loseText.SetActive(true);
    }

    public void restartScene()
    {
        SceneManager.LoadScene(scene.name);
    }
}


