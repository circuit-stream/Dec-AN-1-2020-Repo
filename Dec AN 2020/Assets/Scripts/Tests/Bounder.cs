using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounder : MonoBehaviour
{
    public Material m_redMat;
    public Material m_whiteMat;
    public Material m_greenMat;
    public Renderer m_rend;


    private void OnTriggerEnter(Collider other)
    {
        m_rend.material = m_greenMat;
        StopAllCoroutines();
    }
    private void OnTriggerExit(Collider other)
    {
        m_rend.material = m_redMat;
        StartCoroutine(BackToWhite());
    }

    IEnumerator BackToWhite()
    {
        yield return new WaitForSeconds(2);
        m_rend.material = m_whiteMat;
    }
}
