using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Moves the ARSessionOrigin in such a way that it makes the given content appear to be
/// at a given location acquired via a raycast.
/// </summary>
[RequireComponent(typeof(ARSessionOrigin))]
[RequireComponent(typeof(ARRaycastManager))]
public class MakeAppearOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("A transform which should be made to appear to be at the touch point.")]
    Transform m_Content;

    public Slider rotateSlider;
    public Slider scaleSlider;
    public ARPlaneManager planes;
    public GameObject[] planeList;
    bool isPlaneActive = true;
    public Image dumbIndicator;
    public TMP_Text contents;

    /// <summary>
    /// A transform which should be made to appear to be at the touch point.
    /// </summary>
    public Transform content
    {
        get { return m_Content; }
        set { m_Content = value; }
    }

    [SerializeField]
    [Tooltip("The rotation the content should appear to have.")]
    Quaternion m_Rotation;

    /// <summary>
    /// The rotation the content should appear to have.
    /// </summary>
    public Quaternion rotation
    {
        get { return m_Rotation; }
        set
        {
            m_Rotation = value;
            if (m_SessionOrigin != null)
                m_SessionOrigin.MakeContentAppearAt(content, content.transform.position, m_Rotation);
        }
    }

    void Awake()
    {
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
        m_RaycastManager = GetComponent<ARRaycastManager>();

        dumbIndicator.color = new Color(0, 1, 0);
    }

    void Update()
    {
        planeList = GameObject.FindGameObjectsWithTag("PLANE");

        if (!isPlaneActive)
        {
            foreach (GameObject go in planeList)
            {
                go.GetComponent<ARPlaneMeshVisualizer>().enabled = false;
            }
        }


        scaleContent();
        rotateContent();

        contents.text = planeList.Length.ToString();

        if (Input.touchCount == 0 || m_Content == null)
            return;

        var touch = Input.GetTouch(0);

        if (IsOverUI(touch) == true)
            return;     

        if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;

            // This does not move the content; instead, it moves and orients the ARSessionOrigin
            // such that the content appears to be at the raycast hit position.
            m_SessionOrigin.MakeContentAppearAt(content, hitPose.position, m_Rotation);
        }
    }

    public void rotateContent()
    {
        content.transform.rotation = Quaternion.Euler(0, rotateSlider.value, 0);
    }

    public void scaleContent()
    {
        content.transform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, scaleSlider.value);
    }

    private bool IsOverUI(Touch touch)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = touch.position;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count > 0)
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.GetComponent<CanvasRenderer>())
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void toggle()
    {
        if (isPlaneActive)
        {
            foreach (GameObject go in planeList)
            {
                go.GetComponent<ARPlaneMeshVisualizer>().enabled = false;
            }
            isPlaneActive = false;       
            dumbIndicator.color = new Color(1, 0, 0);
        }
        else
        {
            foreach (GameObject go in planeList)
            {
                go.GetComponent<ARPlaneMeshVisualizer>().enabled = true;
            }
            isPlaneActive = true;
            dumbIndicator.color = new Color(0, 1, 0);
        }
    }
    public void pause()
    {
        planes.enabled = false;
    }

    public void unpause()
    {
        planes.enabled = true;
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARSessionOrigin m_SessionOrigin;

    ARRaycastManager m_RaycastManager;
}
