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
        private Entity m_entity;
        [SerializeField] private TextMeshProUGUI m_hintText;

        private List<Actionable> m_actionsInRange;
        private Transform m_mainCam;
        private Vector2 m_camForward;
        private Vector2 m_camRight;

        private bool m_started;

        private void Awake()
        {
            m_input = GetComponent<PlayerInput>();
            m_entity = GetComponent<Entity>();
            m_actionsInRange = new List<Actionable>();

            m_started = false;
        }

        private void Start()
        {
            m_started = true;
            OnStartOrEnable();
        }

        private void OnEnable() { OnStartOrEnable(); }

        private void OnDisable()
        {
            m_input.actions["Move"].performed -= Move;
            m_input.actions["Move"].canceled -= Move;

            m_input.actions["Interact"].performed -= Interact;
            m_input.actions["CamSwitch"].performed -= CamSwitch;
            m_input.actions["Help"].performed -= PrintHelp;
        }

        private void OnStartOrEnable()
        {
            if (!m_started) return;
            
            m_mainCam = GameObject.FindWithTag("MainCamera").transform;
            UpdateForward();
            m_hintText.gameObject.SetActive(false);

            m_input.actions["Move"].performed += Move;
            m_input.actions["Move"].canceled += Move;

            m_input.actions["Interact"].performed += Interact;
            m_input.actions["CamSwitch"].performed += CamSwitch;
            m_input.actions["Help"].performed += PrintHelp;
        }

        private void OnTriggerEnter(Collider p_other)
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

        private void OnTriggerExit(Collider p_other)
        {
            Actionable l_action = p_other.GetComponent<Actionable>();
            if (l_action != null)
            {
                m_actionsInRange.Remove(l_action);

                m_hintText.text = m_actionsInRange.Count > 0 ? m_actionsInRange.First().GameAction.Title : String.Empty;
                m_hintText.gameObject.SetActive(m_actionsInRange.Count > 0);
            }
        }

        private void UpdateForward()
        {
            Vector3 l_camToPlayerDirection = (transform.position - m_mainCam.position).normalized;
            Vector3 l_right = Vector3.Cross(Vector3.up, l_camToPlayerDirection);
            m_camForward = new Vector2(l_camToPlayerDirection.x, l_camToPlayerDirection.z).normalized;
            m_camRight = new Vector2(l_right.x, l_right.z).normalized;
        }

        #region INPUT_ACTION_METHOD
        private void Move(InputAction.CallbackContext p_ctx)
        {
            if (p_ctx.performed)
            {
                UpdateForward();
                Vector2 l_inputAxis = p_ctx.ReadValue<Vector2>();
                m_entity.Direction = l_inputAxis.y * m_camForward + l_inputAxis.x * m_camRight;
            }
            else if (p_ctx.canceled)
            {
                m_entity.Direction = Vector2.zero;
            }
        }

        private void Interact(InputAction.CallbackContext p_ctx)
        {
            if (m_actionsInRange.Count > 0)
            {
                m_actionsInRange.Last().DoAction(m_entity);
            }
        }

        private void CamSwitch(InputAction.CallbackContext p_ctx)
        {
            GameObject.FindWithTag("GameController").GetComponent<GameDirector>()
                .NextCamera((int)p_ctx.ReadValue<float>());
        }

        private void PrintHelp(InputAction.CallbackContext p_ctx)
        {
            GameObject.FindObjectOfType<UILogger>().PrintHelp();
        }
        #endregion
    }
}