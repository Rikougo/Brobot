using Props;
using UnityEngine;
using System.Linq;

namespace Controllers
{
    [RequireComponent(typeof(Entity))]
    public class AgentController : MonoBehaviour
    {
        private Entity m_entity;
        private AI.Planifier m_planifier = new AI.Planifier();

        public Vector3 TargetPosition
        {
            get;
            set;
        }

        private void Awake()
        {
            m_entity = GetComponent<Entity>();
            TargetPosition = transform.position;
        }

        private void Start()
        {
            m_planifier.AllActions = FindObjectsOfType<AI.Actions.Actionable>().Select(p_action => p_action.GameAction).ToList();
            GameObject.FindWithTag("GameController").GetComponent<GameDirector>().OnActionMade.AddListener(m_planifier.SeeAction);
            m_planifier.Agent = m_entity;
        }
        
        public void Update()
        {
            if (transform.position != TargetPosition)
            {
                Vector3 l_3dDirection = (TargetPosition - transform.position).normalized;
                m_entity.Direction = new Vector2(l_3dDirection.x, l_3dDirection.z);
            }
            else
            {
                m_entity.Direction = Vector2.zero;
            }
            if(Input.GetKeyDown("p"))
            {
                Debug.Log(m_planifier.NextAction?.action?.Title);
            }
        }

        public void SetTarget(Vector3 p_target)
        {
            TargetPosition = p_target;
        }
    }
}