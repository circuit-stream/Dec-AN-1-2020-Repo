using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotchLever : MonoBehaviour
{
    public Transform m_start, m_end;

    public List<Transform> m_notches = new List<Transform>();

    private Transform m_closestNotch;

    public float m_notchSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            m_closestNotch = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 heading = m_end.position - m_start.position;
            float magnitudeOfHeading = heading.magnitude;
            heading.Normalize();

            Vector3 startToHand = other.transform.position - m_start.position;
            float dotProduct = Vector3.Dot(startToHand, heading);
            dotProduct = Mathf.Clamp(dotProduct, 0, magnitudeOfHeading);

            transform.position = m_start.position + heading * dotProduct;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            m_closestNotch = m_notches[0];
            float smallestDistance = Vector3.Distance(m_closestNotch.position, transform.position);
            foreach (Transform notch in m_notches)
            {
                float comparableDistance = Vector3.Distance(notch.position, transform.position);
                if(comparableDistance < smallestDistance)
                {
                    m_closestNotch = notch;
                    smallestDistance = comparableDistance;
                }
            }
        }
    }

    private void Update()
    {
        if(m_closestNotch)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_closestNotch.position, m_notchSpeed * Time.deltaTime);
        }
    }
}
