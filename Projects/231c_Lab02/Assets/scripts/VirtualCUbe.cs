using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCUbe : MonoBehaviour
{
    private Camera cam;

    private Renderer rend;
    private Material baseMat;
    public Material touchMat;

    public bool robotnik = false;
    AudioSource source;
    public bool muted;
    float speed = 1;

    private void Start()
    {
        cam = Camera.main;
        rend = GetComponent<MeshRenderer>();
        baseMat = rend.material;

        source = GetComponent<AudioSource>();
        if (source)
        { source.Play(); }
    }
    private void Update()
    {
        if (source) { source.mute = muted; }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(cam.transform.position, hit.point * hit.distance, Color.yellow, 1);
                Debug.Log("Did Hit");
                if (hit.transform.gameObject.tag == "tappable")
                {
                    rend.material = touchMat;
                    if (robotnik == true)
                    {
                        muted = false;
                        transform.Rotate(0, .2f * speed, 0, Space.Self);
                    }
                    
                    Debug.Log("HIT TAP");
                }
                else
                {
                    rend.material = baseMat;
                    if (robotnik == true)
                    {
                        muted = true;
                    }
                }
            }
            else
            {
                Debug.DrawRay(cam.transform.position, hit.point * 1000, Color.white, 1);
                rend.material = baseMat;
                if (robotnik == true)
                {
                    muted = true;
                }
                Debug.Log("Did not Hit");
            }
        }
        else
        {
            rend.material = baseMat;
            if (robotnik == true)
            {
                muted = true;
            }
        }
    }
}
