using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class VRClicked : MonoBehaviour
{
    public UnityEvent m_clicked;
    // Start is called before the first frame update
    void OnClick()
    {
        m_clicked.Invoke();
    }

    public void LoadScene(int x)
    {
        SceneManager.LoadScene(x, LoadSceneMode.Single);
    }
}
