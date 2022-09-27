using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameDirector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_timerText;
    [SerializeField] private float m_timeBetweenTwoPresses = 1.0f;
    private float m_timer;
    private Button m_lastPress;

    public UnityEvent OnSuccess;

    private void Awake()
    {
        m_timer = 0.0f;
    }

    public void OnNewButtonPressed(Button p_button)
    {
        if (m_timer > 0.0f && m_lastPress != p_button)
        {
            OnSuccess?.Invoke();
            gameObject.SetActive(false);
        } else if (m_timer > 0.0f) return;

        m_timer = m_timeBetweenTwoPresses;
        m_lastPress = p_button;
    }

    public void Update()
    {
        if (m_timer > 0.0f)
        {
            m_timer -= Time.deltaTime;
            m_timerText.text = String.Format("Time remaining {0:0.##}", m_timer);    
        }
        else
        {
            m_timerText.text = "Button not pressed.";
        }
    }
}
