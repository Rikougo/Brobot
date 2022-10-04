using UnityEngine;

namespace Props
{
    public class DoubleDoor : MonoBehaviour
    {
        [SerializeField] private GameObject m_leftDoor;
        [SerializeField] private GameObject m_rightDoor;

        [SerializeField] private Transform m_leftStart;
        [SerializeField] private Transform m_rightStart;
        [SerializeField] private Transform m_leftEnd;
        [SerializeField] private Transform m_rightEnd;

        [SerializeField] private float m_openDuration = 1.0f;
        private float m_timer;
    
        private bool m_isClosed;

        public bool IsClosed => m_isClosed;

        private void Awake()
        {
            m_isClosed = true;
            m_leftDoor.transform.position = m_leftStart.position;
            m_rightDoor.transform.position = m_rightStart.position;
        }

        private void Update()
        {
            if (!m_isClosed)
            {
                if (m_timer < m_openDuration)
                {
                    m_timer += Time.deltaTime;
                    m_leftDoor.transform.position =
                        Vector3.Lerp(m_leftStart.position, m_leftEnd.position, m_timer / m_openDuration);
                    m_rightDoor.transform.position =
                        Vector3.Lerp(m_rightStart.position, m_rightEnd.position, m_timer / m_openDuration);

                }
            }
        }

        public void OpenDoor()
        {
            m_isClosed = false;
            m_timer = 0.0f;
        }
    }
}
