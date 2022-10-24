using System;
using AI.Actions;
using Controllers;
using UnityEngine;
using UnityEngine.Events;

namespace Props
{
    [RequireComponent(typeof(Actionable))]
    public class Button : MonoBehaviour
    {
        private Actionable m_actionable;

        private void Awake()
        {
            m_actionable = GetComponent<Actionable>(); 
        }

        public void PressButton(Entity p_from)
        {
            m_actionable.DoAction(p_from);
        }
    }
}
