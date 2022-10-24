using Controllers;
using UnityEngine;

namespace AI.Actions
{
    public class Actionable : MonoBehaviour
    {
        [SerializeField] private GameAction m_action;
        private GameDirector m_director;
        
        public GameAction GameAction => m_action;

        private void Start()
        {
            m_director = GameObject.FindWithTag("GameController").GetComponent<GameDirector>();

            if (m_action is null || m_director is null)
            {
                Debug.LogError($"Actionable ${gameObject.GetInstanceID()} is missing action or director ref.");
                this.enabled = false;
            }
            m_action.Owner = gameObject;
        }

        public void DoAction(Entity p_from)
        {
            if (!this.enabled) return;
            m_director.DoAction(p_from, m_action);
        } 
    }
}