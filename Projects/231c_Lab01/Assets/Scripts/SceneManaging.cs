using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour
{
    public void game1()
    {
        SceneManager.LoadScene(1);
    }

    public void game2()
    {
        SceneManager.LoadScene(0);
    }
}
