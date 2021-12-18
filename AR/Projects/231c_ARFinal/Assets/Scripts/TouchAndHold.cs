using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAndHold : MonoBehaviour
{
    AudioSource source;
    public bool muted;
    float speed = 1;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        source.mute = muted;
        if (Input.GetMouseButton(0))
        {
            muted = false;
            //Debug.Log("play");
            transform.Rotate(0, .2f * speed, 0, Space.Self);
        }
        else
        {
            muted = true;      
            //Debug.Log("paused");
        }
    }
}
