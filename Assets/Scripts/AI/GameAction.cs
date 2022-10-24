using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace AI
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameAction", menuName = "Bro-Bot/GameAction", order = 1)]
    public class GameAction : ScriptableObject
    {
        [SerializeField] private EnvironmentState m_preConditions;
        [SerializeField] private EnvironmentState m_postConditions;

        [SerializeField] private string m_title;

        public string Title => m_title;
        public EnvironmentState PreConditions => m_preConditions;
        public EnvironmentState PostConditions => m_postConditions;
    }
}