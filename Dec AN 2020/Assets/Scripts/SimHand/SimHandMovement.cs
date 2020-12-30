using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimHandMovement : MonoBehaviour
{
    public float m_moveSpeed = 1.5f;
    public float m_turnSpeed = 15f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * m_moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * Time.deltaTime * m_moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * m_moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * m_moveSpeed);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.transform.Translate(Vector3.down * Time.deltaTime * m_moveSpeed);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * m_moveSpeed);
        }

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * m_turnSpeed, Space.World);
        transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * m_turnSpeed);
    }
}
