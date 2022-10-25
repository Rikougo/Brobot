using Props;
using UnityEngine;
using System.Linq;
using AI.Actions;

namespace Controllers
{
    [RequireComponent(typeof(Entity))]
    public class AgentController : MonoBehaviour
    {
        private Entity m_entity;
        private AI.Planifier m_planifier = new AI.Planifier();

        public AI.Planifier Planifier => m_planifier;

        public Vector3 TargetPosition
        {
            get;
            private set;
        }

        private GameObject m_target;
        public GameObject TargetObject
        {
            get => m_target;
            set
            {
                m_target = value;

                if (m_target is not null) TargetPosition = m_target.transform.position;
                else TargetPosition = transform.position;
            }
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
            GameObject.FindWithTag("GameController").GetComponent<GameDirector>().AfterActionMade.AddListener(AfterActionMade);
            m_planifier.Agent = m_entity;
        }
        
        public void Update()
        {
            float l_distance = (new Vector2(transform.position.x, transform.position.z) -
                                new Vector2(TargetPosition.x, TargetPosition.z)).magnitude;
            if (l_distance > 0.7f)
            {
                Vector3 l_3dDirection = (TargetPosition - transform.position).normalized;
                m_entity.Direction = new Vector2(l_3dDirection.x, l_3dDirection.z);
            }
            else
            {
                m_entity.Direction = Vector2.zero;

                if (TargetObject is not null)
                {
                    TargetObject.GetComponent<Actionable>().DoAction(this.m_entity);
                    TargetObject = null;
                }
            }
        }

        public void AfterActionMade()
        {
            TargetObject = m_planifier.NextAction?.action.Owner;
        }
        
        public void SetTarget(Vector3 p_target)
        {
            TargetPosition = p_target;
        }
    }
}