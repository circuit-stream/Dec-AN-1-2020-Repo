using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimShoot : MonoBehaviour
{
    public GameObject m_prefabFireball;
    public Transform m_spawnTransform;
    public float m_shootForce = 250f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Instantiate
            GameObject fireball = Instantiate(m_prefabFireball, m_spawnTransform.position, m_spawnTransform.rotation);
            //add force to it
            fireball.GetComponent<Rigidbody>().AddForce(m_spawnTransform.forward * m_shootForce);
            //Destroy it after sometime
            Destroy(fireball, 5f);
        }
    }
}
