using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;
using UnityEngine.Events;
using Utils;

public class EnvironmentDependent : MonoBehaviour
{
    [SerializeField] private EnvironmentState m_precondition;

    public UnityEvent OnAction;

    private bool m_consumed = false;
    
    public void OnEnvironmentChange(Environment p_environment)
    {
        if (m_consumed) return;
        
        m_consumed = true;
        if (p_environment.HasPreconditions(m_precondition))
        {
            OnAction?.Invoke();
        }
    }
}
