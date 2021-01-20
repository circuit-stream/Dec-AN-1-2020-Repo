using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimGrab : MonoBehaviour
{
    public Animator m_anim;
    public float m_throwForce = 25;

    private GameObject m_touchingObject;
    private GameObject m_heldObject;

    public List<Vector3> m_grabPosition = new List<Vector3>();

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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            m_anim.SetBool("isGrabbing", true);
            if(m_touchingObject)
            {
                AdvGrab();
            }
        }
        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            m_anim.SetBool("isGrabbing", false);
            if(m_heldObject)
            {
                AdvRelease();
            }
        }

        if(m_heldObject)
        {
            if(m_grabPosition.Count < 20)
            {
                m_grabPosition.Add(this.transform.position);
            }
            else
            {
                m_grabPosition.RemoveAt(0);
                m_grabPosition.Add(this.transform.position);
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
        fx.connectedBody = m_heldObject.GetComponent<Rigidbody>();
        fx.breakForce = 10000;
        fx.breakTorque = 10000;
    }

    void AdvRelease()
    {
        Destroy(GetComponent<FixedJoint>());
        m_heldObject.transform.SetParent(null);


        Vector3 dir = m_grabPosition[m_grabPosition.Count - 1] - m_grabPosition[0];

        Rigidbody rb = m_heldObject.GetComponent<Rigidbody>();

        rb.AddForce(dir * m_throwForce);
        m_grabPosition.Clear();


        m_heldObject = null;
    }

    private void OnJointBreak(float breakForce)
    {
        m_heldObject.transform.SetParent(null);
        m_heldObject = null;
    }
}
