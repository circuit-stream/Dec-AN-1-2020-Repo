using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGrab : MonoBehaviour
{
    public float m_throwForce;
    public Animator m_anim;

    public string m_grip;
    private bool m_gripHeld;

    GameObject m_touchingObject;
    GameObject m_heldObject;

    public List<Vector3> m_grabPositions = new List<Vector3>();
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Interactable")
        {
            m_touchingObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_touchingObject = null;
    }

    void Update()
    {
        if(Input.GetAxis(m_grip) > 0.8f && m_gripHeld == false)
        {
            m_gripHeld = true;
            m_anim.SetBool("isGrabbing", true);
            if(m_touchingObject)
            {
                AdvGrab();
            }
        }
        else if(Input.GetAxis(m_grip) < 0.8f && m_gripHeld == true)
        {
            m_gripHeld = false;
            m_anim.SetBool("isGrabbing", false);
            if(m_heldObject)
            {
                AdvRelease();
            }
        }

        if(m_heldObject)
        {
            Debug.Log("1");
            if(m_grabPositions.Count < 20)
            {
                Debug.Log("2");
                m_grabPositions.Add(this.transform.position);
            }
            else
            {
                Debug.Log("3");
                m_grabPositions.RemoveAt(0);
                m_grabPositions.Add(this.transform.position);
            }
        }
    }

    void Grab()
    {
        m_heldObject = m_touchingObject;
        m_heldObject.GetComponent<Rigidbody>().isKinematic = true;
        m_heldObject.transform.SetParent(this.transform);
    }

    void Release()
    {
        m_heldObject.transform.SetParent(null);
        m_heldObject.GetComponent<Rigidbody>().isKinematic = false;
        m_heldObject = null;
    }

    void AdvGrab()
    {
        m_heldObject = m_touchingObject;
        m_heldObject.transform.SetParent(this.transform);
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 10000;
        fx.breakTorque = 10000;
    }

    void AdvRelease()
    {
        Destroy(GetComponent<FixedJoint>());
        m_heldObject.transform.SetParent(null);

        Vector3 dir = m_grabPositions[m_grabPositions.Count - 1] - m_grabPositions[0];

        Rigidbody rb = m_heldObject.GetComponent<Rigidbody>();
        rb.AddForce(dir * m_throwForce);

        m_grabPositions.Clear();

        m_heldObject = null;
    }

    private void OnJointBreak(float breakForce)
    {
        m_heldObject.transform.SetParent(null);
        m_heldObject = null;
    }
}
