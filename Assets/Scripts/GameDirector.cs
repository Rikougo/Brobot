using System.Collections.Generic;
using System.Linq;
using AI;
using Controllers;
using UnityEngine;
using UnityEngine.Events;

public class GameDirector : MonoBehaviour
{
    public UnityEvent<Entity, GameAction> OnActionMade;

    private List<EnvironmentDependent> m_envDependantProps;
    [SerializeField] private AgentController m_agent;

    private void Awake()
    {
        m_envDependantProps = GameObject.FindObjectsOfType<EnvironmentDependent>().ToList();
    }

    public void DoAction(Entity p_from, GameAction p_action)
    {
        Debug.Log($"{p_from.Name} did {p_action}");
        OnActionMade?.Invoke(p_from, p_action);
        
        foreach(EnvironmentDependent l_object in m_envDependantProps)
        {
            l_object.OnEnvironmentChange(m_agent.Planifier.Environment);
        }
    }

    public void Update() { }
}