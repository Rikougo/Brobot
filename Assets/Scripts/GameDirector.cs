using System;
using System.Collections;
using System.Collections.Generic;
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
    public UnityEvent OnSuccess;
    public UnityEvent<Entity, GameAction> OnActionMade;

    public void DoAction(Entity p_from, GameAction p_action)
    {
        Debug.Log($"{p_from.Name} did {p_action}");
        OnActionMade?.Invoke(p_from, p_action);
    }

    public void Update() { }
}