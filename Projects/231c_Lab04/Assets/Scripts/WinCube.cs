using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCube : MonoBehaviour
{
    GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gm.winCondition();
        }
    } 
}
