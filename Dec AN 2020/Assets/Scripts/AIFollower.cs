using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollower : MonoBehaviour
{

    public Transform m_player;
    public NavMeshAgent m_ai;

    // Update is called once per frame
    void Update()
    {
        m_ai.SetDestination(m_player.position);
    }
}
