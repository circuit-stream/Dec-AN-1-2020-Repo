using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUI : MonoBehaviour
{
    public string m_clickButton;

    LineRenderer m_line;
    RaycastHit m_hit;

    void Start()
    {
        m_line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(m_clickButton))
        {
            if (Physics.Raycast(transform.position, transform.forward, out m_hit))
            {
                m_line.enabled = true;
                m_line.SetPosition(0, transform.position);
                m_line.SetPosition(1, m_hit.point);
            }
            else
            {
                m_line.enabled = false;
            }
        }
        else if (Input.GetButtonUp(m_clickButton))
        {
            m_line.enabled = false;
            if (Physics.Raycast(transform.position, transform.forward, out m_hit))
            {
                m_hit.transform.SendMessage("OnClick");
            }
        }
    }
}
