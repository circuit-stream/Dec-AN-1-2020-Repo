using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SimTeleport : MonoBehaviour
{
    public Transform m_indicator;
    LineRenderer m_line;
    RaycastHit m_hit;
    bool m_canTeleport;

    void Start()
    {
        m_line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.T))
        {
            if(Physics.Raycast(transform.position, transform.forward, out m_hit))
            {
                m_line.enabled = true;
                m_line.SetPosition(0, transform.position);
                m_line.SetPosition(1, m_hit.point);
                m_indicator.position = m_hit.point;
                m_indicator.gameObject.SetActive(true);
                m_canTeleport = true;
            }
            else
            {
                m_line.enabled = false;
                m_canTeleport = false;
                m_indicator.gameObject.SetActive(false);
            }
        }
        else if(Input.GetKeyUp(KeyCode.T))
        {
            m_line.enabled = false;
            m_indicator.gameObject.SetActive(false);
            if (m_canTeleport == true)
            {
                transform.position = m_hit.point;
            }
        }
    }
}
