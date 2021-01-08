using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class TouchButton : MonoBehaviour
{
    public Transform m_downTransform;
    public Transform m_buttonVisual;
    public UnityEvent m_btnDown;
    public UnityEvent m_btnUp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            m_buttonVisual.position = m_downTransform.position;
            m_btnDown.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_buttonVisual.localPosition = Vector3.zero;
            m_btnUp.Invoke();
        }
    }
}
