using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(CharacterController))]
    public class Entity : MonoBehaviour
    {
        private CharacterController m_controller;

        private Vector2 m_direction;
        [SerializeField] private float m_speed = 1.0f;
        [SerializeField] private GameObject m_modelObject;
        [SerializeField] private string m_name;
        
        [SerializeField] private float m_targetRotation;
        [SerializeField] private float m_rotationSpeed = 250.0f;
        [SerializeField] private float m_damping = 10.0f;

        public string Name => m_name;

        public Vector2 Direction
        {
            get => m_direction;
            set
            {
                m_direction = value;
                
                if (m_modelObject && m_direction.magnitude > 0)
                    m_targetRotation = Mathf.Atan2(m_direction.x, m_direction.y) * Mathf.Rad2Deg;
            }
        }

        private void Awake()
        {
            m_controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            var l_rotation = m_modelObject.transform.rotation;
            Quaternion l_targetRotationQuaternion = Quaternion.Euler(l_rotation.x, m_targetRotation, l_rotation.z);
            m_modelObject.transform.rotation =
                Quaternion.Lerp(l_rotation, l_targetRotationQuaternion, Time.deltaTime * m_damping);

            m_controller.SimpleMove(new Vector3(Direction.x, 0.0f, Direction.y) * m_speed);
        }
    }
}
