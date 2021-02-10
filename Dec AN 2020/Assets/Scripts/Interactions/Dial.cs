using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial : MonoBehaviour
{
    public Transform m_lookAtTarget, m_targetReset;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            m_lookAtTarget.parent = other.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 lookPosition = m_lookAtTarget.position - transform.position;
            lookPosition.y = 0;
            Quaternion rot = Quaternion.LookRotation(lookPosition);
            transform.rotation = rot;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            m_lookAtTarget.parent = transform;
            m_lookAtTarget.position = m_targetReset.position;
        }
    }
}
