using System;
using System.Collections.Generic;
using System.Linq;
using AI.Actions;
using Props;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    [RequireComponent(typeof(Entity))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput m_input;
        public Entity m_entity;
        [SerializeField] private TextMeshProUGUI m_hintText;

        private List<Actionable> m_actionsInRange;
        
        private void Awake()
        {
            m_input = GetComponent<PlayerInput>();
            m_entity = GetComponent<Entity>();
            m_actionsInRange = new List<Actionable>();
        }
        
        public void Start()
        {
            m_hintText.gameObject.SetActive(false);
            
            m_input.actions["Move"].performed += (p_ctx) =>
            {
                m_entity.Direction = p_ctx.ReadValue<Vector2>();
            };

            m_input.actions["Move"].canceled += (_) =>
            {
                m_entity.Direction = Vector2.zero;
            };

            m_input.actions["Interact"].performed += (_) =>
            {
                if (m_actionsInRange.Count > 0)
                {
                    m_actionsInRange.Last().DoAction(m_entity);
                }
            };
        }

        public void OnTriggerEnter(Collider p_other)
        {
            Actionable l_action = p_other.GetComponent<Actionable>();
            if (l_action != null)
            {
                m_actionsInRange.Add(l_action);

                if (m_actionsInRange.Count > 0)
                {
                    m_hintText.text = l_action.GameAction.Title;
                    m_hintText.gameObject.SetActive(true);
                }
            }
            
        }

        public void OnTriggerExit(Collider p_other)
        {
            Actionable l_action = p_other.GetComponent<Actionable>();
            if (l_action != null)
            {
                m_actionsInRange.Remove(l_action);

                m_hintText.text = m_actionsInRange.Count > 0 ? m_actionsInRange.First().GameAction.Title : String.Empty;
                m_hintText.gameObject.SetActive(m_actionsInRange.Count > 0);
            }
        }
    }
}