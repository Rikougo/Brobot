using System.Collections.Generic;
using System.Linq;
using Props;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    [RequireComponent(typeof(Entity))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput m_input;
        private Entity m_entity;
        [SerializeField] private GameObject m_hintText;

        private List<Button> m_buttonsInRange;
        
        private void Awake()
        {
            m_input = GetComponent<PlayerInput>();
            m_entity = GetComponent<Entity>();
            m_buttonsInRange = new List<Button>();
        }
        
        public void Start()
        {
            m_hintText.SetActive(false);
            
            m_input.actions["Move"].performed += (ctx) =>
            {
                m_entity.Direction = ctx.ReadValue<Vector2>();
            };

            m_input.actions["Move"].canceled += (ctx) =>
            {
                m_entity.Direction = Vector2.zero;
            };

            m_input.actions["Interact"].performed += (ctx) =>
            {
                if (m_buttonsInRange.Count > 0)
                {
                    m_buttonsInRange.First().PressButton();
                }
            };
        }

        public void OnTriggerEnter(Collider p_other)
        {
            Button l_button = p_other.GetComponent<Button>();
            if (l_button != null)
            {
                m_buttonsInRange.Add(l_button);

                if (m_buttonsInRange.Count > 0)
                {
                    m_hintText.SetActive(true);
                }
            }
            
        }

        public void OnTriggerExit(Collider p_other)
        {
            Button l_button = p_other.GetComponent<Button>();
            if (l_button != null)
            {
                m_buttonsInRange.Remove(l_button);
                
                if (m_buttonsInRange.Count < 1)
                {
                    m_hintText.SetActive(false);
                }
            }
        }
    }
}