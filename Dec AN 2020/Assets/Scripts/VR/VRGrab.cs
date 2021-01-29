using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGrab : MonoBehaviour
{
    public float m_throwForce;
    public Animator m_anim;

    public string m_grip;
    private bool m_gripHeld;

    public string m_trigger;
    private bool m_triggerHeld;

    public string m_menuButton;

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
                m_heldObject.SendMessage("Released");
                AdvRelease();
            }
        }

        if (Input.GetAxis(m_trigger) > 0.5f && !m_triggerHeld)
        {
            m_triggerHeld = true;

            if(m_heldObject)
            {
                m_heldObject.SendMessage("TriggerDown");
            }
        }
        else if(Input.GetAxis(m_trigger) < 0.5f && m_triggerHeld)
        {
            m_triggerHeld = false; 

            if(m_heldObject)
            {
                m_heldObject.SendMessage("TriggerUp");
            }
        }

        if(Input.GetButtonDown(m_menuButton))
        {
            if(m_heldObject)
            {
                m_heldObject.SendMessage("MenuButtonDown");
            }
        }

        //Throwing Calculator
        if (m_heldObject)
        {
            if(m_grabPositions.Count < 20)
            {
                m_grabPositions.Add(this.transform.position);
            }
            else
            {
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
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.connectedBody = m_heldObject.GetComponent<Rigidbody>();
        fx.breakForce = 10000;
        fx.breakTorque = 10000;
        m_heldObject.transform.SetParent(this.transform);
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
