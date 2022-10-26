using System.Collections.Generic;
using System.Linq;
using AI;
using Cinemachine;
using Controllers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public UnityEvent<Entity, GameAction> OnActionMade;
    public UnityEvent AfterActionMade;

    private List<EnvironmentDependent> m_envDependantProps;
    private List<CinemachineVirtualCamera> m_virtualCameras;
    private int m_currentCamera;
    [SerializeField] private AgentController m_agent;

    [SerializeField] private Image m_blackScreen;
    [SerializeField] private int m_nextScene;
    [SerializeField] private float m_loadingDuration = 1.0f;
    private bool m_loadingNextScene = false;
    private float m_loadingTimer;
    private AsyncOperation m_sceneLoader;

    private void Start()
    {
        m_envDependantProps = GameObject.FindObjectsOfType<EnvironmentDependent>().ToList();
        m_virtualCameras = GameObject.FindObjectsOfType<CinemachineVirtualCamera>().ToList();

        if (m_virtualCameras.Count > 0)
        {
            m_currentCamera = 0;
            m_virtualCameras.First().Priority = 10;

            for (int i = 1; i < m_virtualCameras.Count; i++) m_virtualCameras[i].Priority = 0;
        }
    }

    public void NextCamera(int p_delta)
    {
        m_currentCamera = (p_delta < 0 && m_currentCamera == 0) ? (m_virtualCameras.Count - 1) : (m_currentCamera + p_delta) % m_virtualCameras.Count;
        Debug.Log($"{p_delta} {m_currentCamera}");

        m_virtualCameras[m_currentCamera].Priority = 10;

        for (int i = 0; i < m_virtualCameras.Count; i++)
        {
            if (i != m_currentCamera) m_virtualCameras[i].Priority = 0;
        }
    }

    public void DoAction(Entity p_from, GameAction p_action)
    {
        Debug.Log($"{p_from.Name} did {p_action}");
        OnActionMade?.Invoke(p_from, p_action);
        
        foreach(EnvironmentDependent l_object in m_envDependantProps)
        {
            l_object.OnEnvironmentChange(m_agent.Planifier.Environment);
        }
        
        AfterActionMade?.Invoke();
    }

    public void LoadNextScene()
    {
        m_loadingNextScene = true;
        m_loadingTimer = 0.0f;
        m_blackScreen.enabled = true;
        Color l_color = m_blackScreen.color;
        l_color.a = 0.0f;
        m_blackScreen.color = l_color;

        /*m_sceneLoader = SceneManager.LoadSceneAsync(m_nextScene);
        m_sceneLoader.allowSceneActivation = false;*/
    }

    private void Update()
    {
        if (m_loadingNextScene)
        {
            m_loadingTimer += Time.deltaTime;
            Color l_color = m_blackScreen.color;
            l_color.a = m_loadingTimer / m_loadingDuration;
            m_blackScreen.color = l_color;

            if (m_loadingTimer > m_loadingDuration)
            {
                Debug.Log("Next scene activation.");
                l_color.a = 1.0f;
                m_blackScreen.color = l_color;
                // m_sceneLoader.allowSceneActivation = true;
                SceneManager.LoadScene(m_nextScene);
            }
        }
    }
}