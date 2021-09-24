using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCUbe : MonoBehaviour
{
    public void onSeen()
    {
        gameObject.SetActive(true);
    }

    public void onLost()
    {
        gameObject.SetActive(false);
    }
}
