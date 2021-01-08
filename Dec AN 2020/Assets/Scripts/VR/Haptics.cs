using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Haptics : MonoBehaviour
{
    public XRNode m_leftNode;
    public XRNode m_rightNode;
    private static InputDevice m_vrLeftController;
    private static InputDevice m_vrRighttController;

    private void Awake()
    {
        m_vrLeftController = InputDevices.GetDeviceAtXRNode(m_leftNode);
        m_vrRighttController = InputDevices.GetDeviceAtXRNode(m_rightNode);
    }

    public static void VibrateLeft()
    {
        m_vrLeftController.SendHapticImpulse(0, 0.25f, 1f);
    }
    public static void VibrateRight()
    {
        m_vrRighttController.SendHapticImpulse(0, 0.25f, 1f);
    }
}
