using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticWorldCanvas : MonoBehaviour
{
    private Camera m_camera;
    
    void Start()
    {
        m_camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    
    void Update()
    {
        Vector3 l_canvasToCam = (transform.position - m_camera.transform.position).normalized;

        transform.forward = l_canvasToCam;
    }
}
