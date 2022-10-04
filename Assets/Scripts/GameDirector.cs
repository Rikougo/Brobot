using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AI;
using AI.Actions;
using Controllers;
using Props;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Environment = AI.Environment;

public class GameDirector : MonoBehaviour
{
    private List<GameAction> m_actions;

    public UnityEvent OnSuccess;
    public UnityEvent<Entity, GameAction> OnActionMade;

    private void Start()
    {
        m_actions = FindObjectsOfType<Actionable>().Select(p_action => p_action.GameAction).ToList();
    }

    public void DoAction(Entity p_from, GameAction p_action)
    {
        Debug.Log($"{p_from.Name} did {p_action}");
        OnActionMade?.Invoke(p_from, p_action);
    }

    public void Update() { }
}