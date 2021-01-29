using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilBrush : MonoBehaviour
{
    public GameObject m_prefabTrail;
    public Transform m_spawn;

    private GameObject m_currentTrail;

    private List<GameObject> m_trailsDrawn = new List<GameObject>();

    void TriggerDown()
    {
        m_currentTrail = Instantiate(m_prefabTrail, m_spawn.position, m_spawn.rotation, m_spawn);
    }

    void TriggerUp()
    {
        if (m_currentTrail)
        {
            m_currentTrail.transform.SetParent(null);
            m_trailsDrawn.Add(m_currentTrail);
        }
    }
    void Released()
    {
        if (m_currentTrail)
        {
            m_currentTrail.transform.SetParent(null);
            m_trailsDrawn.Add(m_currentTrail);
        }
    }

    void MenuButtonDown()
    {
        if(m_trailsDrawn.Count > 0)
        {
            GameObject lineToBeDeleted = m_trailsDrawn[m_trailsDrawn.Count - 1];
            m_trailsDrawn.Remove(lineToBeDeleted);
            Destroy(lineToBeDeleted);
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Paint")
        {
            m_prefabTrail.GetComponent<TrailRenderer>().material = collision.gameObject.GetComponent<Renderer>().material;
        }
    }
}
