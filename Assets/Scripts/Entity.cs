using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Entity : MonoBehaviour
{
    private CharacterController m_controller;

    private Vector2 m_direction;
    [SerializeField] private float m_speed = 1.0f;
    [SerializeField] private GameObject m_modelObject;

    public Vector2 Direction
    {
        get => m_direction;
        set
        {
            m_direction = value;
            if (m_modelObject && m_direction.magnitude > 0)
                m_modelObject.transform.forward = new Vector3(Direction.x, 0.0f, Direction.y).normalized;
        }
    }

    private void Awake()
    {
        m_controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        m_controller.SimpleMove(new Vector3(Direction.x, 0.0f, Direction.y) * m_speed);
    }
}
