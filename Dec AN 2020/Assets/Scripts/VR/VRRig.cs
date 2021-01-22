using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRRig : MonoBehaviour
{
    public SphereCollider m_feetCollider;

    public Transform m_vrHead;

    // Start is called before the first frame update
    void Awake()
    {
        XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 feetCollisionSpot = m_vrHead.transform.localPosition;
        feetCollisionSpot.y = 0.1f;
        m_feetCollider.center = feetCollisionSpot;
    }
}
