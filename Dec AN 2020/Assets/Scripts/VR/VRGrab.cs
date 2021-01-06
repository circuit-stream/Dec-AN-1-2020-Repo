using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGrab : MonoBehaviour
{
    public Animator m_anim;

    public string m_grip;
    private bool m_gripHeld;

    GameObject m_touchingObject;
    GameObject m_heldObject;

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
                Grab();
            }
        }
        else if(Input.GetAxis(m_grip) < 0.8f && m_gripHeld == true)
        {
            m_gripHeld = false;
            m_anim.SetBool("isGrabbing", false);
            if(m_heldObject)
            {
                Release();
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
}
