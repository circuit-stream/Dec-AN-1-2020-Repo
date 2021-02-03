using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class TapToPlace : MonoBehaviour
{
    public GameObject m_prefab;

    private GameObject m_placedPrefab;

    private Vector2 m_startingPos;

    public float m_moveSpeed = 0.1f;

    static List<ARRaycastHit> s_hits = new List<ARRaycastHit>();

    public ARRaycastManager m_ARRaycastManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (m_placedPrefab)
                {
                    m_startingPos = touch.position;
                }
                else if (m_ARRaycastManager.Raycast(touch.position, s_hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_hits[0].pose;
                    m_placedPrefab = Instantiate(m_prefab, hitPose.position, hitPose.rotation);
                    m_startingPos = touch.position;
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                Vector2 delta = touch.position - m_startingPos;
                if (delta.magnitude > 100)
                {
                    float x = delta.x;
                    float y = delta.y;

                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        Vector3 horizontal = transform.right;
                        horizontal.y = 0;
                        horizontal = horizontal.normalized * m_moveSpeed;

                        if (x < 0)
                        {
                            m_placedPrefab.transform.position -= horizontal;
                        }
                        else
                        {
                            m_placedPrefab.transform.position += horizontal;
                        }
                    }
                    else
                    {
                        Vector3 forward = transform.forward;
                        forward.y = 0;
                        forward = forward.normalized * m_moveSpeed;
                        if (y < 0)
                        {
                            m_placedPrefab.transform.position -= forward;
                        }
                        else
                        {
                            m_placedPrefab.transform.position += forward;
                        }
                    }
                }
            }
        }
    }
}
