using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifter : MonoBehaviour
{
    [SerializeField] private float m_liftSpeed = 2.0f;
    [SerializeField] private Transform m_liftBody;
    private bool m_lifting = false;

    private void Update()
    {
        if (m_lifting)
        {
            m_liftBody.position = m_liftBody.position + Vector3.up * (Time.deltaTime * m_liftSpeed);
        }
    }
    
    public void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject.tag);
        if (collider.gameObject.CompareTag("Player"))
        {
            StartLifting();
        }
    }

    private void StartLifting()
    {
        m_lifting = true;
        
        GameObject.FindWithTag("GameController").GetComponent<GameDirector>().LoadNextScene();
    }
}
