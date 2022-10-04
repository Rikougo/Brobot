using Props;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Entity))]
    public class AgentController : MonoBehaviour
    {
        private Entity m_entity;

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
        }

        public void SetTarget(Vector3 p_target)
        {
            TargetPosition = p_target;
        }
    }
}