using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public Light m_light;

    void TriggerDown()
    {
        m_light.enabled = !m_light.enabled;
    }
}
