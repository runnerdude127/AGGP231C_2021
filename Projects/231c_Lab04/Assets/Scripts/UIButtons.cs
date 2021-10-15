using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class UIButtons : MonoBehaviour
{
    public ARPlaneManager arplane;

    void pauseAR()
    {
        arplane.enabled = false;
    }

    void resumeAR()
    {
        arplane.enabled = false;
    }
}
