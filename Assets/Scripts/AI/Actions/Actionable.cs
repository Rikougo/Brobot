using UnityEngine;

namespace AI.Actions
{
    public class Actionable : MonoBehaviour
    {
        [SerializeField] private GameAction m_action;

        public GameAction GameAction => m_action;

        public void DoAction() {} 
    }
}