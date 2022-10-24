using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using Controllers;
using TMPro;
using UnityEngine;

public class ButtonInfo : MonoBehaviour
{
    private TMPro.TextMeshProUGUI m_text;
    
    public string initMessage = "Hello world !";

    private void Awake()
    {
        m_text = GetComponent<TextMeshProUGUI>();
        m_text.text = initMessage;
    }

    public void OnActionMade(Entity p_from, GameAction p_action)
    {
        m_text.text = $"{p_from.Name} did {p_action.Title}.";
    }
}